using System;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Enemy {
    public class DropBomb : Enemy
    {
        protected override void AttackEvent() { }
        public override void TakeDamage() {
            health -= 1;
        }

        protected override void DestroyEvent() {
            for (int j = 1; j < 7; j++) {
                for (int i = 0; i < 10; i++) {
                    var b = EnemyBulletManager.Manager.EnemyBulletPool.Get();
                    b.SetInitials(0, 0, j * 0.5f, i, this.transform.position);
                }
            }
            Destroy(this.gameObject);
        }

        private void Update() {
            if (health <= 0) {
                DestroyEvent();
            }
        }

        private void OnTriggerEnter2D(Collider2D col) {

            if (col.CompareTag("Player")) {
                DestroyEvent();
            }
        }


    }
}
