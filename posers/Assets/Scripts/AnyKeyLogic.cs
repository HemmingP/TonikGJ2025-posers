using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class AnyKeyLogic : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        bool anyGamepadIsPressed = Gamepad.all.Any(gamepad => gamepad.buttonSouth.wasPressedThisFrame);
        if (Keyboard.current.spaceKey.wasPressedThisFrame || anyGamepadIsPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart scene
        }
    }
}
