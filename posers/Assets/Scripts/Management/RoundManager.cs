using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private JointValidator[] poses;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private bool startOnStart = false;
    [SerializeField] private StartTimer timer;


    private List<JointValidator> availablePoses = new List<JointValidator>();

    private JointValidator currentPose;

    private void Start() {
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
}
