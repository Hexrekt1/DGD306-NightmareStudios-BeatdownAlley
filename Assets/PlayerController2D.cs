using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody component
    }

    void Update()
    {
        // Get movement input
        moveInput.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        moveInput.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
        moveInput.Normalize(); // Prevents faster diagonal movement
    }

    void FixedUpdate()
   {
    Vector2 newPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
    rb.MovePosition(newPosition);
   }
}
