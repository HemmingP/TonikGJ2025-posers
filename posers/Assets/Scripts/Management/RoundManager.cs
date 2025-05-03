using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private JointValidator[] poses;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private bool startOnStart = false;
    [SerializeField] private StartTimer timer;

    [SerializeField] private JointCollector[] jointCollectors;

    private List<JointValidator> availablePoses = new List<JointValidator>();
    private JointValidator currentPose;

    private void Start() {
        GenerateAvailablePoses();

        if (startOnStart) {
            StartRound();
        }
    }

    public void StartRound() {
        if(currentPose != null) {
            currentPose.gameObject.SetActive(false);
        }

        var selectedPose = availablePoses[Random.Range(0, availablePoses.Count)];
        currentPose = selectedPose;
        currentPose.gameObject.SetActive(true);
        
        timer.TriggerTimer();
    }

    public void ValidateRound() {
        Assert.IsTrue(currentPose != null, "Failed to validate round, because no pose is currently active. This should never happen!");

        foreach(var collector in jointCollectors) {
            var recordedJoints = collector.recordedJoints;
            var result = currentPose.CompareAccuracy(recordedJoints);
            Debug.Log($"Round Result: {result.accuracy}%");
        }

        StartRound();
    }

    private void GenerateAvailablePoses() {
        foreach(var pose in poses) {
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
