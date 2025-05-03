using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStartButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    // This method can be assigned in the Inspector's OnClick and passed the scene name
    public void OnClickStart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
