using System;
using _Scripts.Function;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class Barbette : MonoBehaviour {
        /// <summary>
        /// The Sprite sequence of the gun.
        /// </summary>
        [SerializeField] private Sprite[] gunDirectionImage;

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
            _isOnUse = true;
            _gunFrame = 0;
            _fireRadius = 0.9f;
            _timer = 0;
        }

        void Update() {
            var tf = transform;
            
            //key frame
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if(_gunFrame < 8) _gunFrame++;
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if(_gunFrame > -8) _gunFrame--;
            }
            _spriteRenderer.sprite = gunDirectionImage[(int)Mathf.Abs(_gunFrame)];
            
            //x flip
            int xScale = (_gunFrame > 0) ? 1 : -1;
            tf.localScale = new Vector3(xScale, transform.localScale.y, tf.localScale.z);

            //Fire
            if (_isOnUse && Input.GetAxis("Fire1") > 0) {
                _timer++;
                if(_timer % 20 == 0) 
                    PlayerBulletManager.Manager.BarbetteFire
                    (transform.position, -_gunFrame * 10.7f, _fireRadius);
            }
            
        }
    }
}
