using UnityEngine;
using UnityEngine.InputSystem;

public class GameStartHandler : MonoBehaviour
{
    public GameObject pressStartObject; 
    private PlayerControls inputControls;
    private bool hasStarted = false;

    private void Awake()
    {
        inputControls = new PlayerControls();
    }

    private void OnEnable()
    {
        inputControls.GamepadsJoystick.Enable();
        inputControls.GamepadsJoystick.StartGame.performed += OnStartPressed;
    }

    private void OnDisable()
    {
        inputControls.GamepadsJoystick.StartGame.performed -= OnStartPressed;
        inputControls.GamepadsJoystick.Disable();
    }

    private void OnStartPressed(InputAction.CallbackContext context)
    {
        if (hasStarted) return;

        hasStarted = true;
        pressStartObject.SetActive(false);
        

        
    }
}
