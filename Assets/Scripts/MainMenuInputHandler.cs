using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class MainMenuInputHandler : MonoBehaviour
{
    private bool gameStarted = false;

    void Update()
    {
        if (gameStarted) return;

        // Check all Gamepads
        foreach (var gamepad in Gamepad.all)
        {
            if (AnyButtonPressed(gamepad))
            {
                StartGame();
                return;
            }
        }

        // Check all Joysticks
        foreach (var joystick in Joystick.all)
        {
            if (AnyButtonPressed(joystick))
            {
                StartGame();
                return;
            }
        }
    }

    private bool AnyButtonPressed(InputDevice device)
    {
        foreach (var control in device.allControls)
        {
            if (control is ButtonControl button && button.wasPressedThisFrame)
                return true;
        }
        return false;
    }

    private void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameLevel");
    }
}
