using System;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Enemy {
    public class DropBomb : Enemy
    {
        protected override void Destroy() {
            
        }

        private void Update() {
            if (health <= 0) {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("PlayerBullet")) {
                health -= 5;
            }

            if (col.CompareTag("Player")) {
                Destroy(this.gameObject);
            }

            if (col.CompareTag("Enviorment")) {
                Destroy(this.gameObject);
            }
                
        }
    }
}
