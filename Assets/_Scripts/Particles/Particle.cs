using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Scripts.Particles {
    public class Particle : MonoBehaviour {
        [SerializeField] private int type;
        public Vector3 birthPoint;
        private int _timer;
        private int _num;
        private int _ord;

        /// <summary>
        /// 1 or -1.
        /// </summary>
        private int _mul;

        public void SetBasic(int t, int num, int mul, int ord) {
            //Remember to refresh.
            _timer = 0;
            type = t;
            _num = num;
            _mul = mul;
            _ord = ord;
            transform.localScale = Vector3.one;
        }


        // Update is called once per frame
        void Update() {
            _timer++;
            switch (type) {
                case 1:
                    Vector3 pos;
                    pos.x = _ord * 0.001f * _timer * Mathf.Cos((_num * 36f + _mul * _timer / 3f) * Mathf.Deg2Rad);
                    pos.y = _ord * 0.001f * _timer * Mathf.Sin((_num * 36f + _mul * _timer / 3f) * Mathf.Deg2Rad);
                    pos.z = 0f;
                    transform.position = birthPoint + pos;

                    var scale = 1f - _timer / 500f;
                    if (scale <= 0.0001f)
                        ParticleManager.Manager.ParticlePool.Release(this);
                    var mul = (Mathf.Abs(Mathf.Sin(_timer * Mathf.Deg2Rad)) + 0.5f) / 1.5f;
                    transform.localScale = Vector3.one * scale * mul;
                    break;
                default:
                    ParticleManager.Manager.ParticlePool.Release(this);
                    break;
            }
        }

        //OnBecameVisible is called when the renderer became visible by any camera.
        private void OnBecameInvisible() {
            //ParticleManager.Manager.ParticlePool.Release(this);
        }
    }
}
