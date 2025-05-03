using UnityEngine;

public class JointCollector : MonoBehaviour
{
    private JointRecord[] records;

    private void Start() {
      records = GetComponentsInChildren<JointRecord>();
    }

    public JointRecord[] recordedJoints => records;
}