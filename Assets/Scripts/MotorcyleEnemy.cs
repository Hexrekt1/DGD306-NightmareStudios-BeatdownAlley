using UnityEngine;

public class MotorcycleEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 6f;

    [Tooltip("If checked, motorcycle moves right; otherwise moves left.")]
    public bool goRight = false;

    private Vector3 direction;

    [Header("Damage")]
    public int damage = 1;

    [Header("Lifetime")]
    public float lifetime = 4f; 

    private void OnEnable()
    {
        
        direction = goRight ? Vector3.right : Vector3.left;

        
        Invoke(nameof(SelfDestruct), lifetime);
    }

    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && !player.isDead)
            {
                Debug.Log(" Motorcycle hit the player!");
                player.TookDamage(damage);
            }
        }
    }

    private void SelfDestruct()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
