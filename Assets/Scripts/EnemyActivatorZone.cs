using UnityEngine;
using System.Collections;

public class EnemyActivatorZone : MonoBehaviour
{
    public float cameraLockX = 100f;

    public GameObject[] phase1Enemies;
    public GameObject[] phase2Enemies;
    public GameObject[] phase3Enemies;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated || !other.CompareTag("Player")) return;

        activated = true;
        GetComponent<Collider>().enabled = false;

        // Lock the camera position
        CameraFollow cam = FindObjectOfType<CameraFollow>();
        if (cam != null)
            cam.maxXAndY.x = cameraLockX;

        StartCoroutine(ActivateEnemiesInPhases());
    }

    private IEnumerator ActivateEnemiesInPhases()
    {
        // Phase 1
        foreach (var enemy in phase1Enemies)
        {
            if (enemy != null)
            {
                var mover = enemy.GetComponent<ActivatedEnemyMover>();
                if (mover != null)
                    mover.Activate();
                else
                    enemy.SetActive(true);
            }
        }
        yield return new WaitForSeconds(4f);

        // Phase 2
        foreach (var enemy in phase2Enemies)
        {
            if (enemy != null)
            {
                var mover = enemy.GetComponent<ActivatedEnemyMover>();
                if (mover != null)
                    mover.Activate();
                else
                    enemy.SetActive(true);
            }
        }
        yield return new WaitForSeconds(4f);

        // Phase 3
        foreach (var enemy in phase3Enemies)
        {
            if (enemy != null)
            {
                var mover = enemy.GetComponent<ActivatedEnemyMover>();
                if (mover != null)
                    mover.Activate();
                else
                    enemy.SetActive(true);
            }
        }

        // Check enemies cleared
        StartCoroutine(CheckEnemiesCleared());
    }

    private IEnumerator CheckEnemiesCleared()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            // Check if any enemies still exist and are active
            bool anyAlive = false;

            foreach (var enemy in phase1Enemies)
                if (enemy != null && enemy.activeInHierarchy)
                    anyAlive = true;

            foreach (var enemy in phase2Enemies)
                if (enemy != null && enemy.activeInHierarchy)
                    anyAlive = true;

            foreach (var enemy in phase3Enemies)
                if (enemy != null && enemy.activeInHierarchy)
                    anyAlive = true;

            if (!anyAlive)
                break;

            yield return new WaitForSeconds(1f);
        }

        // Unlock the camera
        CameraFollow cam = FindObjectOfType<CameraFollow>();
        if (cam != null)
            cam.maxXAndY.x = 200f; // your default max scroll
    }
}
