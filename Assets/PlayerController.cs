using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;

    public float moveSpeed = 1f;
    public float jumpForce = 1f;
    private bool isGrounded = true;


    private void Start()
{
    rb.velocity = Vector3.zero;
}
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 horizontalVelocity = new Vector3(moveInput.x * moveSpeed, 0f, moveInput.y * moveSpeed);
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public void Attack()
    {
        Debug.Log($"{gameObject.name} attacks!");
        // Animation or hitbox logic can go here
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we landed on something
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
