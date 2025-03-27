using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA 
{
    public class InputHandler : MonoBehaviour
    {
        public UnitController unitController;

        private void Update()
        {
            float h = Input.GetAxis("Horizontal"); // A/D or Left/Right
            float v = Input.GetAxis("Vertical");   // W/S or Up/Down

            Vector3 targetDirection = new Vector3(h, 0, v).normalized; // Normalize to prevent speed boost diagonally

            unitController.TickMovement(Time.deltaTime, targetDirection);
        }
    }
}
