using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float maxSpeed;
    public float damageTime = 0.5f;
    public float minHeight, maxHeight;
    public int maxHealth;
    public float attackRate = 1f;
    public string enemyName;
    public Sprite enemyImage;
    public AudioClip collisonSound, deathSound;

    private int currentHealth;
    private float currentSpeed;
    private Rigidbody rb;
    protected Animator anim;
    private Transform groundCheck;
    private bool onGround;
    protected bool facingRight = false;
    private Transform target;
    protected bool isDead = false;
    private float zForce;
    private float walkTimer;
    private bool damaged = false;
    private float damageTimer;
    private float nextAttack;
    private AudioSource audioS;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        currentHealth = maxHealth;
        audioS = GetComponent<AudioSource>();
        currentSpeed = maxSpeed;
    }

    void Update()
    {
        if (GameManager.Instance.players.Count == 0)
        {
            
            return;
        }

        target = GetClosestPlayer();
        if (target == null) return;

        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        anim.SetBool("Grounded", onGround);
        anim.SetBool("Dead", isDead);

        facingRight = (target.position.x < transform.position.x) ? false : true;
        transform.eulerAngles = facingRight ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);

        if (damaged && !isDead)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageTime)
            {
                damaged = false;
                damageTimer = 0;
            }
        }

        walkTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (isDead || target == null) return;

        Vector3 targetDistance = target.position - transform.position;
        float hForce = targetDistance.x / Mathf.Abs(targetDistance.x);

        if (walkTimer >= Random.Range(1f, 2f))
        {
            zForce = Random.Range(-1, 2);
            walkTimer = 0;
        }

        if (Mathf.Abs(targetDistance.x) < 1.5f)
        {
            hForce = 0;
        }

        if (!damaged)
            rb.velocity = new Vector3(hForce * currentSpeed, 0, zForce * currentSpeed);

        anim.SetFloat("Speed", Mathf.Abs(currentSpeed));

        if (Mathf.Abs(targetDistance.x) < 1.5f && Mathf.Abs(targetDistance.z) < 1.5f && Time.time > nextAttack)
        {
            anim.SetTrigger("Attack");
            currentSpeed = 0;
            nextAttack = Time.time + attackRate;
        }

        rb.position = new Vector3(rb.position.x, rb.position.y, Mathf.Clamp(rb.position.z, minHeight + 1, maxHeight - 1));
    }

    public void TookDamage(int damage)
    {
        if (!isDead)
        {
            damaged = true;
            currentHealth -= damage;
            anim.SetTrigger("HitDamage");
            PlaySong(collisonSound);
            FindObjectOfType<UIManager>()?.UpdateEnemyUI(maxHealth, currentHealth, enemyName, enemyImage);

            if (currentHealth <= 0)
            {
                isDead = true;
                rb.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                PlaySong(deathSound);
            }
        }
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

    void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }

    private Transform GetClosestPlayer()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Player player in GameManager.Instance.players)
        {
            if (player == null || player.isDead) continue;

            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = player.transform;
            }
        }

        return closest;
    }
}
