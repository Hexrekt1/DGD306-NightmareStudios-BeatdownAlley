using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{

 public class DynamicSortOrder : MonoBehaviour
 {
    public SpriteRenderer spriteRenderer;
    public Transform origin;
    public void Update()
        {
            Vector3 position = transform.position;
            if(origin !=null)
            { 
                position = origin.position;

            }
            int sortOrder = Mathf.RoundToInt(position.z * 10);
            spriteRenderer.sortingOrder = -sortOrder;
            
        }

    }

}