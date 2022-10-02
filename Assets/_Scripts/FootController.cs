using System;
using _Scripts.Function;
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
                if (_pointer >= animLength[(int)state] - 1)
                    _pointer = 0;
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

        public PlayerState GetPlayerState() {
            return _playerState;
        }

        private void SwitchState(int num) {
            if (num >= 0 && num <= 5 && _playerState != (PlayerState)num) {
                _playerState = (PlayerState)num;
                _pointer = 0;
                _timer = 0;
            }
        }

        private SpriteRenderer _sprRenderer;
        private Rigidbody2D _rigidbody2D;
        
        private void Awake() {
            _sprRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private float _runSpeed;
        private float _jumpSpeed;
        
        void Start() {
            _runSpeed = 5f;
            _jumpSpeed = 8f;
            _timer = 0;
            _pointer = 0;
            _playerState = PlayerState.Idle;
        }

        private float _horMovement;
        private float _verMovement;
        private bool _isJumpPressed;
        
        void Update() {
            /*if(_horMovement == 0)*/ _horMovement = Input.GetAxisRaw("Horizontal");
            /*if(_verMovement == 0)*/ _verMovement = Input.GetAxisRaw("Vertical");
            if(!_isJumpPressed) _isJumpPressed = Input.GetKeyDown(KeyCode.UpArrow);
        }
        
        void FixedUpdate() {
            var hor = _horMovement;
            var ver = _verMovement;

            if (_playerState != PlayerState.Jump && _playerState != PlayerState.JumpIdle) {
                if (ver > 0) {
                    SwitchState(3);
                }
                else {
                    if (hor != 0) {
                        if (ver < 0) SwitchState(2);
                        else if (ver == 0) SwitchState(1);
                    }
                    else {
                        if (ver < 0) SwitchState(4);
                        else if (ver == 0) SwitchState(0);
                    }
                }
            }else if (_playerState == PlayerState.Jump) {
                if (_pointer == animLength[(int)_playerState] - 2) {
                    SwitchState(5);
                }
            }

            if((_playerState == PlayerState.Jump || 
                _playerState == PlayerState.JumpIdle) &&
               Calc.Equal(_rigidbody2D.velocity.y, 0f))
                SwitchState(0);
            
            //_verMovement = 0;
            //_horMovement = 0;

            var speed = _runSpeed;
            if (_playerState == PlayerState.Crouch) speed /= 2f;

            var temp = hor < 0 ? 1f : -1f;
            transform.localScale = new Vector3(temp, 1, 1);
            _rigidbody2D.position += Vector2.right * hor * speed * Time.fixedDeltaTime;
            
            if (_isJumpPressed && Calc.Equal(_rigidbody2D.velocity.y, 0f)) {
                _rigidbody2D.velocity += Vector2.up * _jumpSpeed;
                _isJumpPressed = false;
            }


            SetAnimation(_sprRenderer, _playerState,10);
        }
    }
}
