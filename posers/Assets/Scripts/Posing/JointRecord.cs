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

  public float angle {
    get {
      if (manualParent == null) {
        return transform.localRotation.eulerAngles.z;
      }

      // convert the rotation of this object to be calculated as if it was the child of the manual parent object
      var parentMatrix = manualParent.transform.localToWorldMatrix;
      var thisMatrix = transform.localToWorldMatrix;

      var parentRotation = parentMatrix.rotation.eulerAngles.z;
      var thisRotation = thisMatrix.rotation.eulerAngles.z;

      return thisRotation - parentRotation;
    }
  }


}
