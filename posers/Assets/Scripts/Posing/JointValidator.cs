using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System.Collections;

public struct JointValidationResult
{
    public float accuracy;
    public float difference;
}


public class JointValidator : MonoBehaviour
{
    [SerializeField] private JointCollector testReferenceJoints;
    [SerializeField] private float testMinimumAccuracy = 80.0f;
    [SerializeField] private float maxTolerance = 170.0f;

    private Dictionary<string, JointRecord> jointRecords;

    void Awake()
    {
        jointRecords = new Dictionary<string, JointRecord>();
        foreach (var jointRecord in GetComponentsInChildren<JointRecord>())
        {
            Assert.IsTrue(!jointRecords.ContainsKey(jointRecord.joint.name), $"Joint {jointRecord.joint} is already recorded");
            jointRecords.Add(jointRecord.joint.name, jointRecord);
        }

        Debug.Log("Joints Registered: " + jointRecords.Count);
    }


    void Start()
    {
        if (testReferenceJoints != null)
        {
            StartCoroutine(TestValidateJoints());
        }
    }

    IEnumerator TestValidateJoints()
    {

        while (true)
        {
            yield return new WaitForSeconds(2);
            CompareAccuracy(testReferenceJoints.recordedJoints, testMinimumAccuracy);
        }

    }

    public JointValidationResult CompareAccuracy(JointRecord jointRecord)
    {

        Assert.IsTrue(jointRecords.ContainsKey(jointRecord.joint.name), $"Unable to compare {jointRecord.joint.name} because it is not registered in the Joint Validator");
        var recordJoint = jointRecords[jointRecord.joint.name];

        var angle = jointRecord.Angle;
        var recordAngle = recordJoint.Angle;

        // Calculate the shortest difference between the angles
        var delta = Mathf.DeltaAngle(angle, recordAngle);
        var diff = Mathf.Abs(delta);

        // Calculate accuracy: 1 - (normalized difference)
        // Normalized difference is diff / 180 (max possible difference)
        var accuracy = (1.0f - (diff / maxTolerance)) * 100.0f; // Use floats for division

        Debug.Log($"Comparing {jointRecord.joint.name} with {recordJoint.name}\n Angle: {angle:F1} | Record: {recordAngle:F1} | Difference: {diff:F1} | Accuracy: {accuracy:F1}%");

        return new JointValidationResult
        {
            accuracy = accuracy,
            difference = diff
        };
    }

    public JointValidationResult CompareAccuracy(JointRecord[] jointRecords, float minimumAccuracy = 0.0f)
    {
        var totalAccuracy = 0.0f;
        var totalDifference = 0.0f;

        foreach (var jointRecord in jointRecords)
        {
            var result = CompareAccuracy(jointRecord);
            if (result.accuracy > minimumAccuracy)
            {
                totalAccuracy += result.accuracy;
                totalDifference += result.difference;
            }
        }

        var averageAccuracy = totalAccuracy / jointRecords.Length;
        var averageDifference = totalDifference / jointRecords.Length;

        Debug.Log($"{gameObject.name}: Average Accuracy: {averageAccuracy}% | Average Difference: {averageDifference}");

        return new JointValidationResult
        {
            accuracy = averageAccuracy,
            difference = averageDifference
        };
    }
}
