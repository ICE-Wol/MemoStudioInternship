using System;
using _Scripts.Function;
using UnityEngine;

namespace _Scripts.Laser {
    public class LaserUnit : MonoBehaviour {
        public LaserHead head;
        public int order;
        public bool isActive;

        private void Update() {
            Vector2 pre = (order != 0) ? head.GetPreUnit(order).transform.position : Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    (order != 0) ? head.GetPreUnit(order).transform.position : head.transform.position;
            transform.position = 
                Calc.Approach(transform.position, pre, Vector3.one * 64f);
        }

        private void OnDrawGizmos() {
            // Draw a yellow sphere at the transform's position
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}
