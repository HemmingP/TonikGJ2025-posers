using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

public struct JointValidationResult
{
    public float accuracy;
    public float difference;
}


public class JointValidator : MonoBehaviour
{
    [SerializeField] private GameObject testReferenceJoints;

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

            var foundJointsOnTestObject = testReferenceJoints.GetComponentsInChildren<JointRecord>();
            Assert.IsTrue(foundJointsOnTestObject.Length == jointRecords.Count, $"The number of joints on the test object ({foundJointsOnTestObject.Length}) does not match the number of joints registered in the Joint Validator ({jointRecords.Count})");

            var totalResults = 0.0f;
            foreach (var jointRecord in foundJointsOnTestObject)
            {
                var result = CompareAccuracy(jointRecord);
                Debug.Log($"Comparing {jointRecord.joint.name} with {jointRecord.name}\n Accuracy: {result.accuracy}% | Difference: {result.difference}");

                totalResults += result.accuracy;
            }
            Debug.Log($"{gameObject.name}: Total Accuracy: {totalResults / foundJointsOnTestObject.Length}%");
        }
    }

    public JointValidationResult CompareAccuracy(JointRecord jointRecord)
    {

        Assert.IsTrue(jointRecords.ContainsKey(jointRecord.joint.name), $"Unable to compare {jointRecord.joint.name} because it is not registered in the Joint Validator");
        var recordJoint = jointRecords[jointRecord.joint.name];

        var angle = Mathf.Abs(jointRecord.angle);
        var recordAngle = Mathf.Abs(recordJoint.angle);

        var diff = Mathf.Abs(angle - recordAngle);

        // calculate accuracy (how close the angle is to the record angle)
        var accuracy = Mathf.Abs(1 - (diff / 180));

        Debug.Log($"Comparing {jointRecord.joint.name} with {jointRecord.name}\n Accuracy: {accuracy}% | Difference: {diff}");

        return new JointValidationResult
        {
            accuracy = accuracy * 100, // to percentage
            difference = diff
        };
    }

    public JointValidationResult CompareAccuracy(JointRecord[] jointRecords)
    {
        var totalAccuracy = 0.0f;
        var totalDifference = 0.0f;

        foreach (var jointRecord in jointRecords)
        {
            var result = CompareAccuracy(jointRecord);
            totalAccuracy += result.accuracy;
            totalDifference += result.difference;
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
