using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
  public class AIHandler : MonoBehaviour
 {
    public UnitController unitController;

    public Transform target;
        private void Update()
        {
            if(target == null)
            return;

            unitController.agent.SetDestination(target.position);
        }
 }

}
