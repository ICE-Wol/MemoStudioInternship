using System;
using UnityEngine;

namespace _Scripts.Enemy {
    public abstract class Enemy : MonoBehaviour {
        [SerializeField] protected int health;
        [SerializeField] protected bool useGravity;
        protected void Attack() { }
        protected abstract void Destroy();

        private void OnDestroy() {
            Destroy();
        }
    }
}
