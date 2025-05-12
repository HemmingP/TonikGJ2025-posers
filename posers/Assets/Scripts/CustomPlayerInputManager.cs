using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class CustomPlayerInputManager : MonoBehaviour
{
    private bool leftKeyboardJoined = false;
    private bool rightKeyboardJoined = false;

    // We'll just store the devices that should control each player
    private List<InputDevice> joinedDevices = new List<InputDevice>();
    private List<string> joinedControlSchemes = new List<string>();
    public TextMeshProUGUI waitingForPlayersTextField;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (joinedDevices.Count >= 2)
            return;

        // Left side keyboard (WASD)
        if (!leftKeyboardJoined && Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            leftKeyboardJoined = true;
            joinedDevices.Add(Keyboard.current);
            joinedControlSchemes.Add("Left Keyboard");
            CheckAndLoadScene();
        }

        // Right side keyboard (Arrow keys)
        if (!rightKeyboardJoined && Keyboard.current.rightShiftKey.wasPressedThisFrame)
        {
            rightKeyboardJoined = true;
            joinedDevices.Add(Keyboard.current);
            joinedControlSchemes.Add("Right Keyboard");
            CheckAndLoadScene();
        }

        // Gamepads
        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame && !joinedDevices.Contains(gamepad))
            {
                joinedDevices.Add(gamepad);
                joinedControlSchemes.Add("Gamepad");
                CheckAndLoadScene();
            }
        }
    }

    private void CheckAndLoadScene()
    {
        waitingForPlayersTextField.text = "Waiting for 1 player";
        if (joinedDevices.Count >= 2)
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            // var playerInputs = FindObjectsByType<PlayerInput>(FindObjectsSortMode.SceneHierarchy);
            PlayerInput[] playerInputs = {
                GameObject.Find("Character 1").GetComponent<PlayerInput>(),
                GameObject.Find("Character 2").GetComponent<PlayerInput>()
            };

            if (playerInputs.Length < 2)
            {
                Debug.LogError("Expected 2 PlayerInput components in Main scene!");
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                var playerInput = playerInputs[i];

                playerInput.user.UnpairDevices();
                InputUser.PerformPairingWithDevice(joinedDevices[i], playerInput.user);
                playerInput.SwitchCurrentControlScheme(joinedControlSchemes[i], joinedDevices[i]);
                Debug.Log("currentControlScheme: " + playerInput.currentControlScheme);
                playerInput.SwitchCurrentActionMap(joinedControlSchemes[i] == "Right Keyboard" ? "Move Ragdoll 2" : "Move Ragdoll");
                playerInput.ActivateInput();

                Debug.Log($"{playerInput.name} paired with: {string.Join(",", playerInput.devices)}");
                Debug.Log($"{playerInput.name} current map: {playerInput.currentActionMap.name}");
            }
        }
    }
}
