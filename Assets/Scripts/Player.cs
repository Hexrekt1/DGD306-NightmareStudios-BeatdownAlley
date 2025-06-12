using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public UIManager uiManager;
    

    public float maxSpeed = 4f;
    public float jumpForce = 400f;
    public float minHeight, maxHeight;
    public int maxHealth = 10;
    public string playerName;
    public Sprite playerImage;
    public AudioClip collisionSound, jumpSound, healthItem;

    private int currentHealth;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private Transform groundCheck;
    private bool onGround;
    public bool isDead = false;

    private bool facingRight = true;

    
    private Vector2 moveInput = Vector2.zero;
    private bool jumpPressed = false;
    private bool attackPressed = false;
    private bool healthPressed = false;

    private AudioSource audioS;

    private void Start()
    {

        var cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null && cameraFollow.m_Player == null)
        {
            cameraFollow.SetTarget(transform);
        }


        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
        currentHealth = maxHealth;
        audioS = GetComponent<AudioSource>();

        if (uiManager != null)
            uiManager.Initialize(this);
    }

    private void Update()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        anim.SetBool("OnGround", onGround);
        anim.SetBool("Dead", isDead);

        if (jumpPressed && onGround)
        {
            jumpPressed = false;
            Jump();
        }

        if (attackPressed)
        {
            attackPressed = false;
            anim.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            float h = moveInput.x;
            float z = moveInput.y;

            if (!onGround)
                z = 0;

            rb.velocity = new Vector3(h * currentSpeed, rb.velocity.y, z * currentSpeed);

            if (onGround)
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));

            if (h > 0 && !facingRight)
            {
                Flip();
            }
            else if (h < 0 && facingRight)
            {
                Flip();
            }

            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
            rb.position = new Vector3(
                Mathf.Clamp(rb.position.x, minWidth, maxWidth),
                rb.position.y,
                Mathf.Clamp(rb.position.z, minHeight + 1, maxHeight - 1));
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
        PlaySong(jumpSound);
    }

    private void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }

    public void TookDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            anim.SetTrigger("HitDamage");
            uiManager.UpdateHealth(currentHealth);
            PlaySong(collisionSound);
            if (currentHealth <= 0)
            {
                isDead = true;
                GameManager.Instance.sharedLives--;
                GameManager.Instance.NotifyAllUIManagersLivesChanged();

                if (GameManager.Instance.sharedLives > 0)
                {
                    StartCoroutine(RespawnAfterDelay(1f));
                }
                else
                {
                    
                }

                if (facingRight)
                    rb.AddForce(new Vector3(-3, 5, 0), ForceMode.Impulse);
                else
                    rb.AddForce(new Vector3(3, 5, 0), ForceMode.Impulse);
            }
        }
    }

    
    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (GameManager.Instance.sharedLives > 0)
        {
            PlayerRespawn();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Health Item") && healthPressed)
        {
            healthPressed = false;
            Destroy(other.gameObject);
            PlaySong(healthItem);
            currentHealth = maxHealth;
            uiManager.UpdateHealth(currentHealth);
        }
    }

    

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpPressed = true;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            attackPressed = true;
    }

    public void OnHealth(InputAction.CallbackContext context)
    {
        if (context.performed)
            healthPressed = true;
    }
    public void OnExit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Quitting game...");
            Application.Quit();
        }
    }
    public void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }
    public void ZeroSpeed()
    {
        currentSpeed = 0;
    }
    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RegisterPlayer(this);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.UnregisterPlayer(this);
    }
    public void PlayerRespawn()
    {
        

        Camera cam = Camera.main;
        if (cam == null)
        {
            
            return;
        }

        Vector3 respawnPos = transform.position;

        
        
        float distanceFromCamera = 1f; 

        if (facingRight)
        {
            
            Vector3 leftEdgeWorld = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight / 2, cam.nearClipPlane));
            respawnPos.x = leftEdgeWorld.x + distanceFromCamera;
        }
        else
        {
            
            Vector3 rightEdgeWorld = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight / 2, cam.nearClipPlane));
            respawnPos.x = rightEdgeWorld.x - distanceFromCamera;
        }

        
        transform.position = respawnPos;

        currentHealth = maxHealth;
        isDead = false;
        anim.SetBool("Dead", false);
        anim.Play("Idle"); 

        
        rb.velocity = Vector3.zero;

        
        ResetSpeed();

        
        if (uiManager != null)
            uiManager.UpdateHealth(currentHealth);

        Debug.Log($"{playerName} respawned at camera edge.");
    }

}
