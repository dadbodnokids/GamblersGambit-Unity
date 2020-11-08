using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scott.ScratchCard
{
    public class FollowMouse : MonoBehaviour
    {
        public Camera cam;

        public void MoveToMouse()
        {
            float distance = transform.position.z - cam.transform.position.z;
            Vector3 targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            transform.position = cam.ScreenToWorldPoint(targetPos);
        }
    }
}
    
