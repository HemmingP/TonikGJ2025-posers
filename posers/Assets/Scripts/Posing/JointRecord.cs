using UnityEngine;

public class JointRecord : MonoBehaviour
{
  public ScriptableEnum joint;
  
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, 0.1f);
  }

  public float angle => transform.localRotation.eulerAngles.z;
}
