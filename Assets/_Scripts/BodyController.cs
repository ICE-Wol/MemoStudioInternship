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
        private int _timer = 0;

        private void Awake() {
            _animator = GetComponent<Animator>();
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
                if (_timer % 20 == 0)
                    PlayerBulletManager.Manager.PlayerFire
                        (transform.position, dir2, fireRadius);
            }

            if (Input.GetKeyDown(KeyCode.X)) {
                Instantiate(grenade,transform.position,quaternion.Euler(0f,0f,0f));
            }
            
            

            var angle = Vector2.Angle(Vector2.right, dir);
            if (angle > 90f) angle = angle - 180f;
            
            pivot.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}
