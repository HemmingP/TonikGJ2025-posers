using UnityEngine;

public class MenuQuitButton : MonoBehaviour
{
    public void OnClickQuit()
    {
        Debug.Log("QuitGame called");  // Works in Editor
        Application.Quit();            // Works in build
    }
}