using System;
using _Scripts.Function;
using UnityEngine;

namespace _Scripts {
    public class BodyController : MonoBehaviour {
        [SerializeField] private FootController fc;
        [SerializeField] private Vector3[] offset;
        private Animator _animator;
        

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
                    transform.localPosition = offset[1];
                    break;
                case PlayerState.CrouchIdle:
                    transform.localPosition = offset[1];
                    break;
                case PlayerState.Jump:
                    transform.localPosition = offset[2];
                    break;
                case PlayerState.JumpIdle:
                    transform.localPosition = offset[2];
                    break;
                default:
                    transform.localPosition = offset[0];
                    break;
            }
        }
    }
}
