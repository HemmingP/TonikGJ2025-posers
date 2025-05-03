using UnityEngine;

public class ToggleScreen : MonoBehaviour
{
    public void Toggle()
    {
        GetComponent<Animator>().SetTrigger("Toggle Endscreen");
    }
}
