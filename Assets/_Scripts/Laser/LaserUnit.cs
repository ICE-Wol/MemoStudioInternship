using System;
using _Scripts.Function;
using UnityEngine;

namespace _Scripts.Laser {
    public class LaserUnit : MonoBehaviour {
        public LaserHead head;
        public int order;
        public bool isActive;


        private void Start() {
            transform.position = Vector3.zero;
        }

        private void Update() {
            Vector3 pre = (order != 0) ? head.GetPreUnitPosition(order): head.GetFollowTransform().position;
            //value type should be changed by hand.
            transform.position = Calc.Approach(transform.position, pre, Vector3.one * head.GetFollowRate());
        }
    }
}
