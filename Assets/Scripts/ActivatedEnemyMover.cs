using UnityEngine;

public class ActivatedEnemyMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float selfDestructTime = 5f;
    public float targetX = 200f;

    private bool isActive = false;
    private float timer = 0f;

    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;

        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (timer >= selfDestructTime)
            Destroy(gameObject);
    }

    public void Activate()
    {
        isActive = true;
        timer = 0f;
        gameObject.SetActive(true);
    }
}
