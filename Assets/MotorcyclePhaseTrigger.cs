using System.Collections;
using UnityEngine;

public class MotorcyclePhaseTrigger : MonoBehaviour
{
    public GameObject[] phase1Enemies;
    public GameObject[] phase2Enemies;
    public GameObject[] phase3Enemies;

    public CameraFollow cameraFollow; // Reference to your camera follow script

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(SpawnPhases());
            LockCamera();
        }
    }

    private IEnumerator SpawnPhases()
    {
        ActivateEnemies(phase1Enemies);

        yield return new WaitForSeconds(4f);
        ActivateEnemies(phase2Enemies);

        yield return new WaitForSeconds(4f);
        ActivateEnemies(phase3Enemies);

        // Start monitoring for enemy deaths
        StartCoroutine(CheckForEnemiesRemaining());
    }

    private void ActivateEnemies(GameObject[] enemies)
    {
        foreach (var enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }

    private IEnumerator CheckForEnemiesRemaining()
    {
        while (true)
        {
            if (NoEnemiesAlive())
            {
                UnlockCamera();
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private bool NoEnemiesAlive()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    private void LockCamera()
    {
        if (cameraFollow != null)
            cameraFollow.LockCamera();
    }

    private void UnlockCamera()
    {
        if (cameraFollow != null)
            cameraFollow.UnlockCamera();
    }

}
