using System;
using _Scripts.Function;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class LaserLead : MonoBehaviour {
        public int _timer;
        private int _timer2;
        private int _timerLerp;
        private float _speed;
        private float _radius;
        private bool _stage;
        private void Start() {
            _radius = 0f;
            _timer2 = Random.Range(0,3600);
            _speed = Random.Range(0.3f, 0.6f);
        }

        private void Update() {
            _timer++;
            _timer2++;
            _timerLerp++;
            //_radius = Calc.Approach(_radius, 3f + Mathf.Sin(Mathf.Deg2Rad * _timer / 10f) / 2f, f);
            
            //TODO:trigger block?
            if (!_stage) {
                _radius = Mathf.Lerp(0, 3f, _timerLerp / 1000f);
                transform.position = _radius * Calc.Degree2Direction(_timer2 / 10f * _speed);
                if (Calc.Equal(3f, _radius)) _stage = true;
            }
            else {
                transform.position = (3f + Mathf.Sin(Mathf.Deg2Rad * _timer / 10f) / 2f) *
                                     Calc.Degree2Direction(_timer2 / 10f * _speed);
            }
        }
    }
}   