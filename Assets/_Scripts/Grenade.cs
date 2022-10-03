using System;
using _Scripts.Particles;
using UnityEngine;

namespace _Scripts {
    public class Grenade : MonoBehaviour {
        [SerializeField] private float power;
        private Rigidbody2D _rigidbody2D;

        public void Start() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos = transform.position;
            _rigidbody2D.velocity = power * (pos - mouse).normalized;
        }

        private void OnCollisionEnter2D(Collision2D col) {
            if (col.gameObject.CompareTag("Player"))
                return;
            if (col.gameObject.CompareTag("Bullet"))
                return;
            Destroy(this.gameObject);
        }

        private void OnDestroy() {
            for (int i = 1; i <= 3; i++) {
                for (int j = 0; j <= 9; j++) {
                    var particle = ParticleManager.Manager.ParticlePool.Get();
                    particle.birthPoint = this.transform.position;
                    particle.SetBasic(1, j, (i % 2 == 0) ? 1 : -1, i);
                }
            }
        }
    }
}
