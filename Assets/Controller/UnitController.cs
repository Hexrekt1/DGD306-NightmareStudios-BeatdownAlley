using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    public class UnitController : MonoBehaviour
    {
       public NavMeshAgent agent;
        Animator anim;
        public float moveSpeed = 1f; // Movement speed

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();

            agent.updateRotation = false; // Prevent automatic rotation
        }

        public void TickMovement(float delta, Vector3 direction)
        {
            if (direction.magnitude > 0) // If there's movement input
            {
                agent.Move(direction * moveSpeed * delta); // Move the agent manually
            }
        }
    }
}
