using System;
using _Scripts.Function;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class PlayerBullet : MonoBehaviour {
        private float _direction;
        private float _speed;

        public void SetDirection(float dir) {
            _direction = dir + Random.Range(-5f,5f);
        }

        public void SetProperties(Vector3 pos, Quaternion qua,float dir) {
            transform.position = pos;
            transform.rotation = qua;
            _direction = dir + Random.Range(-5f,5f);
        } 
        
        // Start is called before the first frame update
        void Start() {
            _speed = Random.Range(0, 0f);
        }

        // Update is called once per frame
        void FixedUpdate() {
            _speed = Calc.Approach(_speed, 0.5f, 32f);
            var pos = transform.position;
            pos.y += _speed * Mathf.Cos(Mathf.Deg2Rad * _direction);
            pos.x -= _speed * Mathf.Sin(Mathf.Deg2Rad * _direction);
            transform.position = pos;
        }

        private void OnBecameInvisible() {
            PlayerBulletManager.Manager.PlayerBulletPool.Release(this);
        }
    }
}
