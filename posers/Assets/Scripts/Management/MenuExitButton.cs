using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void OnClickQuit()
    {
        Debug.Log("QuitGame called");  // Works in Editor
        Application.Quit();            // Works in build
    }
}