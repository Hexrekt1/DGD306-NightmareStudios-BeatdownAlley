using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls controls;
    private PlayerController player;

    private void Awake()
    {
        controls = new PlayerControls();
        player = GetComponent<PlayerController>();

        controls.Player.Move.performed += ctx => player.SetMoveInput(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => player.SetMoveInput(Vector2.zero);

        controls.Player.Jump.performed += ctx => player.Jump();
        controls.Player.Attack.performed += ctx => player.Attack();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
}
