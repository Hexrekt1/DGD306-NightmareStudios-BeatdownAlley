using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuInputHandler : MonoBehaviour
{
    private PlayerControls inputControls;

    private void Awake()
    {
        inputControls = new PlayerControls();
    }

    private void OnEnable()
    {
        inputControls.GamepadsJoystick.Enable(); 
        inputControls.GamepadsJoystick.StartGame.performed += OnStartGame;
    }

    private void OnDisable()
    {
        inputControls.GamepadsJoystick.StartGame.performed -= OnStartGame;
        inputControls.GamepadsJoystick.Disable();
    }

    private void OnStartGame(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("GameLevel"); 
        Time.timeScale = 1f;
    }
}
