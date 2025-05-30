using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    [HideInInspector] public Transform m_Player;

    private bool isLocked = false; // ← New lock flag

    private bool CheckXMargin()
    {
        if (m_Player == null) return false;
        return Mathf.Abs(transform.position.x - m_Player.position.x) > xMargin;
    }

    private void Update()
    {
        if (m_Player != null && !isLocked)
            TrackPlayer();
    }

    private void TrackPlayer()
    {
        float targetX = transform.position.x;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, m_Player.position.x, xSmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    public void SetTarget(Transform newPlayer)
    {
        m_Player = newPlayer;
    }

    // 🔒 Public methods to control lock state
    public void LockCamera()
    {
        isLocked = true;
    }

    public void UnlockCamera()
    {
        isLocked = false;
    }
}
