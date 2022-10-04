using System;
using _Scripts.Function;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class Barbette : MonoBehaviour {
        /// <summary>
        /// The Sprite sequence of the gun.
        /// </summary>
        [SerializeField] private Sprite[] gunDirectionImage;
        [SerializeField] private Sprite[] playerDirectionImage;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        [SerializeField] private PlayerBullet playerBullet;

        /// <summary>
        /// Whether the player is using the gun.
        /// </summary>
        private bool _isOnUse;

        private float _gunFrame;

        private float _fireRadius;

        private int _timer;
        
        private SpriteRenderer _spriteRenderer;
        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() {
            playerSpriteRenderer.flipX = true;
            playerSpriteRenderer.enabled = false;
            _isOnUse = false;
            _gunFrame = 0;
            _fireRadius = 0.9f;
            _timer = 0;
        }
        
        private GameObject _playerOnTrigger;
        //private Vector3 _playerPosition;
        private bool _playerIn;

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Player")) {
                _playerIn = true;
                _playerOnTrigger = col.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D col) {
            if (col.CompareTag("Player") && !_playerIn)
                _playerIn = false;
        }
        
        void Update() {
            if (_playerIn) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    _isOnUse = !_isOnUse;
                    _playerOnTrigger.SetActive(!_isOnUse);
                    playerSpriteRenderer.enabled = _isOnUse;
                }
            }

            if (!_isOnUse) return;

            var tf = transform;
            
            //key frame
            if (Input.GetKeyDown(KeyCode.D)) {
                if(_gunFrame < 6) _gunFrame++;
            } else if (Input.GetKeyDown(KeyCode.A)) {
                if(_gunFrame > -6) _gunFrame--;
            }
            _spriteRenderer.sprite = gunDirectionImage[(int)Mathf.Abs(_gunFrame)];
            playerSpriteRenderer.sprite = playerDirectionImage[(int)Mathf.Abs(_gunFrame)];
            playerSpriteRenderer.transform.localPosition =
                -0.12f * Mathf.Abs(_gunFrame) * Vector3.right - 0.5f * Vector3.up;
                

            //x flip
            int xScale = (_gunFrame > 0) ? 1 : -1;
            tf.localScale = new Vector3(xScale, transform.localScale.y, tf.localScale.z);

            //Fire
            if (Input.GetAxis("Fire1") > 0) {
                _timer++;
                if(_timer % 20 == 0) 
                    PlayerBulletManager.Manager.BarbetteFire
                    (transform.position, -_gunFrame * 10.7f, _fireRadius);
            }
            
        }
    }
    
}
