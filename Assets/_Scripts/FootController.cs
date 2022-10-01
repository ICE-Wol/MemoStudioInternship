using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerState {
    Idle = 0,
    Run = 1,
    Crouch = 2,
    Jump = 3,
    CrouchIdle = 4,
    JumpIdle = 5,
}

namespace _Scripts {
    public class FootController : MonoBehaviour {
        [SerializeField] private Sprite[] animPlayerRun;
        [SerializeField] private Sprite[] animPlayerJump;
        [SerializeField] private Sprite[] animPlayerCrouch;
        [SerializeField] private int[] animLength;

        [SerializeField] private Sprite sprIdle;
        [SerializeField] private Sprite sprJumpIdle;
        [SerializeField] private Sprite sprCrouchIdle;

        private int _timer;
        private int _pointer;
        
        /// <summary>
        /// Set the target sprite renderer with an animation.
        /// Should be called per frame. 
        /// </summary>
        /// <param name="sprRenderer">The target renderer.</param>
        /// <param name="state">The corresponding state of the sprite sequence aka the animation.</param>
        /// <param name="speed">The fixed frames to change the sprite.</param>
        public void SetAnimation(SpriteRenderer sprRenderer,PlayerState state, int speed) {
            if (_timer % speed == 0) {
                _pointer++;
                if (_pointer >= animLength[(int)state] - 1) _pointer = 0;
            }
            
            switch (state) {
                case PlayerState.Idle:
                    sprRenderer.sprite = sprIdle;
                    break;
                case PlayerState.Crouch:
                    sprRenderer.sprite = animPlayerCrouch[_pointer];
                    break;
                case PlayerState.Jump:
                    sprRenderer.sprite = animPlayerJump[_pointer];
                    break;
                case PlayerState.Run:
                    sprRenderer.sprite = animPlayerRun[_pointer];
                    break;
                case PlayerState.CrouchIdle:
                    sprRenderer.sprite = sprCrouchIdle;
                    break;
                case PlayerState.JumpIdle:
                    sprRenderer.sprite = sprJumpIdle;
                    break;
            }
            _timer++;
        }

        private PlayerState _playerState;

        private void SwitchState(int num) {
            if (num >= 0 && num <= 5 && _playerState != (PlayerState)num) {
                _playerState = (PlayerState)num;
                _pointer = 0;
                _timer = 0;
            }
        }

        private SpriteRenderer _sprRenderer;
        
        private void Awake() {
            _sprRenderer = GetComponent<SpriteRenderer>();
        }

        private float _speed;
        
        void Start() {
            _speed = 0.1f;
            _timer = 0;
            _pointer = 0;
            _playerState = PlayerState.Idle;
        }
        
        
        void FixedUpdate() {
            var hor = Input.GetAxisRaw("Horizontal");
            var ver = Input.GetAxisRaw("Vertical");
            if (hor != 0) {
                var temp = hor < 0 ? 1f : -1f;
                transform.localScale = new Vector3(temp, 1, 1);
                if(ver < 0) SwitchState(2);
                else if(ver == 0) SwitchState(1);
            }
            else{
                if(ver < 0) SwitchState(4);
                else if(ver == 0) SwitchState(0);
            }

            var speed = _speed;
            if (_playerState == PlayerState.Crouch) speed /= 2f;

            transform.position += new Vector3(hor * speed, 0, 0);

            /*if (Input.GetAxisRaw("Vertical") > 0) {
                SwitchState(3);
            }*/
            
            SetAnimation(_sprRenderer, _playerState,5);
        }
    }
}
