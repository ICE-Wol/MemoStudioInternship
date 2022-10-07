using System;
using _Scripts.Function;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts {
    public class BodyController : MonoBehaviour {
        [SerializeField] private FootController fc;
        [SerializeField] private Vector3[] offset;
        [SerializeField] private Transform pivot;
        [SerializeField] private Grenade grenade;
        [FormerlySerializedAs("_fireRadius")] [SerializeField] private float fireRadius;
        
        private Animator _animator;
        private int _timer;
        
        private int _weaponType;
        private int _numLife;
        private int _numGrenade;

        public PlayerValue GetPlayerValue() => new PlayerValue(_numLife, _numGrenade, _weaponType);
        public void TakeDamage(int damage) => _numLife -= damage;

        private void Awake() {
            _animator = GetComponent<Animator>();
            _timer = 0;
            _weaponType = 0;
            _numLife = 100;
            _numGrenade = 10;
        }

        private void Update() {
            _animator.SetBool("IsFiring",
                !Calc.Equal(Input.GetAxis("Fire1"), 0f));

            if (!Calc.Equal(Input.GetAxis("Fire3"), 0f)) {
                _animator.SetTrigger("IsPunching");
            }

            //Debug.Log(fc.GetPlayerState());
            switch (fc.GetPlayerState()) {
                case PlayerState.Crouch:
                    pivot.localPosition = offset[1];
                    break;
                case PlayerState.CrouchIdle:
                    pivot.localPosition = offset[1];
                    break;
                case PlayerState.Jump:
                    pivot.localPosition = offset[2];
                    break;
                case PlayerState.JumpIdle:
                    pivot.localPosition = offset[2];
                    break;
                default:
                    pivot.localPosition = offset[0];
                    break;
            }


            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Important.no z
            mouse.z = 0f;
            Vector3 ori = transform.position;
            ori.z = 0;
            //Important. from to to.
            //Angle has to be signed.
            //float dir = Vector2.SignedAngle(transform.position, mouse);

            Vector3 dir = (mouse - ori);

            if (dir.y <= 0) dir.y = 0;
            if (Input.GetAxisRaw("Horizontal") >= 0) {
                if (dir.x <= 0) dir.x = 0;
            }
            else {
                if (dir.x >= 0) dir.x = 0;
            }

            Vector3 dir2 = (new Vector3( dir.y, -dir.x, 0f)).normalized;

            if (Input.GetAxis("Fire1") > 0) {
                _timer++;
                if (_timer % 20 == 0) {
                    switch (_weaponType) {
                        default:
                            PlayerBulletManager.Manager.PlayerFire
                                (transform.position, dir2, fireRadius);
                            break;
                        case 1:
                            PlayerBulletManager.Manager.PlayerFire
                                (transform.position, dir2, fireRadius);
                            //TODO: package it up.
                            float x = dir2.x * Mathf.Cos(30f * Mathf.Deg2Rad) - dir2.y * Mathf.Sin(30f * Mathf.Deg2Rad);
                            float y = dir2.x * Mathf.Sin(30f * Mathf.Deg2Rad) + dir2.y * Mathf.Cos(30f * Mathf.Deg2Rad);
                            Vector3 dirUp = new Vector3(x, y, 0f);
                            float x1 = dir2.x * Mathf.Cos(330f * Mathf.Deg2Rad) - dir2.y * Mathf.Sin(330f * Mathf.Deg2Rad);
                            float y1 = dir2.x * Mathf.Sin(330f * Mathf.Deg2Rad) + dir2.y * Mathf.Cos(330f * Mathf.Deg2Rad);
                            Vector3 dirDown = new Vector3(x1, y1, 0f);
                            PlayerBulletManager.Manager.PlayerFire
                                (transform.position, dirUp, fireRadius);
                            PlayerBulletManager.Manager.PlayerFire
                                (transform.position, dirDown, fireRadius);
                            break;
                        case 2:
                            PlayerBulletManager.Manager.PlayerFire
                                (transform.position, dir2, fireRadius);
                            if (_timer % 60 == 0) {
                                for (int i = 0; i < 6; i++) {
                                    float x2 = Mathf.Cos((_timer * _timer / 60000f + i * 60f) * Mathf.Deg2Rad);
                                    float y2 = Mathf.Sin((_timer * _timer / 60000f + i * 60f) * Mathf.Deg2Rad);
                                    var dirSix = new Vector3(x2, y2, 0f);
                                    PlayerBulletManager.Manager.PlayerFire
                                        (transform.position, dirSix, fireRadius, 0.03f);
                                }
                            }

                            break;

                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                _weaponType += 1;
                if (_weaponType >= 3) _weaponType = 0;
            }
            
            if (Input.GetKeyDown(KeyCode.X) && _numGrenade > 0) {
                Instantiate(grenade,transform.position,quaternion.Euler(0f,0f,0f));
                _numGrenade -= 1;
            }
            
            

            var angle = Vector2.Angle(Vector2.right, dir);
            if (angle > 90f) angle = angle - 180f;
            
            pivot.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}
