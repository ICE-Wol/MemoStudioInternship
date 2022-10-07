using System;
using _Scripts.Function;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class PlayerBullet : MonoBehaviour {
        private float _direction;
        private float _speed;
        private bool _increase;

        public void SetDirection(float dir) {
            _direction = dir + Random.Range(-5f,5f);
        }

        public void SetProperties(Vector3 pos, Quaternion qua,float dir) {
            transform.position = pos;
            transform.rotation = qua;
            _direction = dir + Random.Range(-5f,5f);
            _increase = true;
        } 
        
        public void SetProperties(Vector3 pos, Quaternion qua,float dir, float spd) {
            transform.position = pos;
            transform.rotation = qua;
            _speed = spd;
            _direction = dir;
            _increase = false;
        }

        // Update is called once per frame
        void FixedUpdate() {
            if(_increase)_speed = Calc.Approach(_speed, 0.3f, 32f);
            var pos = transform.position;
            pos.y += _speed * Mathf.Cos(Mathf.Deg2Rad * _direction);
            pos.x -= _speed * Mathf.Sin(Mathf.Deg2Rad * _direction);
            transform.position = pos;
        }

        private void OnBecameInvisible() {
            PlayerBulletManager.Manager.PlayerBulletPool.Release(this);
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Enemy")) {
                col.gameObject.GetComponent<Enemy.Enemy>().TakeDamage();
                PlayerBulletManager.Manager.PlayerBulletPool.Release(this);
            }
        }
    }
}
