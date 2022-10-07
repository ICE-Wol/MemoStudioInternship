using System;
using UnityEngine;

namespace _Scripts.Enemy {
    public abstract class Enemy : MonoBehaviour {
        [SerializeField] protected int health;
        [SerializeField] protected bool useGravity;
        protected int Timer;
        protected abstract void AttackEvent();
        protected abstract void DestroyEvent();

        public abstract void TakeDamage();
    }
}
