using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinScript : MonoBehaviour
{
    public Transform SpawnPoint1,SpawnPoint2;
    public GameObject W, G;

    private void Awake()
    {
        Instantiate(W,SpawnPoint1.position,SpawnPoint1.rotation);
        Instantiate(G, SpawnPoint2.position, SpawnPoint2.rotation);
    }
}
