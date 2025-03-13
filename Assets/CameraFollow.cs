using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  
    public float smoothSpeed = 0.2f;
    public Vector3 offset = new Vector3(0, 5, -10);

    void LateUpdate() // Ensure the camera moves AFTER player movement
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
