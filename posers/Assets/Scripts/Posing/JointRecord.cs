using UnityEngine;

public class JointRecord : MonoBehaviour
{
  public ScriptableEnum joint;

  [Tooltip("If there is a parent, but it is a neighbouring object, instead of a direct transform parent")]
  public GameObject manualParent;

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, 0.1f);
  }

  public float Angle
  {
    get
    {
      if (manualParent == null)
      {
        // If no manual parent is specified, return the standard local rotation's Z angle.
        return transform.localRotation.eulerAngles.z;
      }
      else
      {
        // Get the world rotation of the manual parent and this joint.
        Quaternion parentWorldRotation = manualParent.transform.rotation;
        Quaternion childWorldRotation = transform.rotation;

        // Calculate the rotation of the child relative to the parent.
        // This is done by multiplying the inverse of the parent's world rotation
        // by the child's world rotation.
        Quaternion relativeRotation = Quaternion.Inverse(parentWorldRotation) * childWorldRotation;

        // Return the Z Euler angle from the calculated relative rotation.
        // This represents the joint's angle as if it were a direct child of the manualParent.
        return relativeRotation.eulerAngles.z;
      }
    }
  }
}
