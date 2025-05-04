using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public struct RoundResult
{
    public JointValidationResult result;
    public string playerName;
}

public class RoundManager : MonoBehaviour
{
    [SerializeField] private JointValidator[] poses;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private bool startOnStart = false;
    [SerializeField] private StartTimer timer;

    [SerializeField] private float minimumAccuracy = 80.0f;
    [SerializeField] private int reductionInSecondsPerRound = 3;
    [SerializeField] private JointCollector[] jointCollectors;

    [SerializeField] private UnityEvent<string> onGameEnded;
    [SerializeField] private UnityEvent onGameTied;

    private List<JointValidator> availablePoses = new List<JointValidator>();

    private JointValidator currentPose;

    private void Start()
    {
        GenerateAvailablePoses();

        if (startOnStart)
        {
            StartRound();
        }
    }

    private void SetNewPose()
    {
        if (currentPose == null)
        {
            currentPose = availablePoses[Random.Range(0, availablePoses.Count)];
            currentPose.gameObject.SetActive(true);
            return;
        }

        var currentlySelectedPose = currentPose;
        do
        {
            currentlySelectedPose = availablePoses[Random.Range(0, availablePoses.Count)];
        } while (currentlySelectedPose == currentPose);

        currentPose.gameObject.SetActive(false);
        currentPose = currentlySelectedPose;
        currentPose.gameObject.SetActive(true);
    }

    public void StartRound()
    {
        this.SetNewPose();
        timer.TriggerTimer();
        timer.ReduceTotalSeconds(reductionInSecondsPerRound);
    }

    public void ValidateRound()
    {
        Assert.IsTrue(currentPose != null, "Failed to validate round, because no pose is currently active. This should never happen!");

        var collections = new List<RoundResult>();

        foreach (var collector in jointCollectors)
        {
            var recordedJoints = collector.recordedJoints;
            var result = currentPose.CompareAccuracy(recordedJoints, minimumAccuracy);

            collections.Add(new RoundResult
            {
                result = result,
                playerName = collector.playerName
            });
        }

        var accuracyBelowMinimum = collections.FindAll(result => result.result.accuracy < minimumAccuracy);

        // is the game a tie?
        if (accuracyBelowMinimum.Count == jointCollectors.Length)
        {
            onGameTied?.Invoke();
            return;
        }

        // was anyone below the minimum accuracy?
        if (accuracyBelowMinimum.Count > 0)
        {
            var winningPlayer = collections.Find(result => result.result.accuracy >= minimumAccuracy);
            var playerName = winningPlayer.playerName;

            onGameEnded?.Invoke($"{playerName} Wins!");
            return;
        }

        // if everyone is above the minimum accuracy, start a new round
        StartRound();
    }

    private void GenerateAvailablePoses()
    {
        foreach (var pose in poses)
        {
            var newPose = Instantiate(pose.gameObject);

            newPose.transform.SetParent(spawnLocation);
            newPose.transform.localPosition = Vector3.zero;
            newPose.transform.localRotation = Quaternion.identity;
            newPose.transform.localScale = Vector3.one;

            newPose.SetActive(false);

            var validator = newPose.GetComponent<JointValidator>();
            Assert.IsTrue(validator != null, "Failed to create pose, because the prefab is missing the JointValidator component");

            availablePoses.Add(validator);
        }
    }
}
