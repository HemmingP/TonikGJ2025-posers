using UnityEngine;

public class JointCollector : MonoBehaviour
{
    [SerializeField] private string ownerOfJoints;

    private JointRecord[] records;

    private void Start() {
      records = GetComponentsInChildren<JointRecord>();
    }

    public JointRecord[] recordedJoints => records;

    public string playerName => ownerOfJoints;
}