using System;
using _Scripts.Function;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts {
    public class EnemyBullet : MonoBehaviour {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private int _sprNum;

        private int _type;
        private float _speed;
        private int _ord;
        private float _dir;
        private int _timer;
        private int _timerRec;

        public void SetInitials(int sprNum, int type, float speed, int ord,Vector3 pos) {
            _timer = 0;
            spriteRenderer.sprite = EnemyBulletManager.Manager.GetBulletSprite(sprNum);
            _type = type;
            _speed = speed;
            _ord = ord;
            transform.position = pos;
        }        
        public void SetInitials(int sprNum, int type, int ord,Vector3 pos) {
            _timer = 0;
            _speed = 0;
            spriteRenderer.sprite = EnemyBulletManager.Manager.GetBulletSprite(sprNum);
            _type = type;
            _ord = ord;
            transform.position = pos;
        }
        
        public void SetInitials(int sprNum, int type, float speed,float dir,Vector3 pos) {
            _timer = 0;
            spriteRenderer.sprite = EnemyBulletManager.Manager.GetBulletSprite(sprNum);
            _type = type;
            _speed = speed;
            _dir = dir;
            transform.position = pos;
        }
        // Update is called once per frame
        void Update() {
            _timer++;
            switch (_type) {
                default:
                    transform.position +=
                        Time.deltaTime * _speed * (Vector3)Calc.Degree2Direction(_dir);
                    transform.rotation = Quaternion.Euler(0,0,_dir - 90f);
                    break;
                case 0:
                    if (_speed >= -6f) {
                        _speed -= 0.003f;

                        transform.position +=
                            Time.deltaTime * _speed *
                            (Vector3)Calc.Degree2Direction(_speed / 0.5f * 10f + _ord * 36f + _timer / 3f);
                        transform.rotation = Quaternion.Euler(0, 0,
                            _speed / 0.5f * 10f + _ord * 36f + _timer / 3f - Mathf.Sign(_speed) * 90f);
                    }
                    else {
                        if (_timerRec == 0) _timerRec = _timer;
                        transform.position +=
                            Time.deltaTime * _speed *
                            (Vector3)Calc.Degree2Direction(_speed / 0.5f * 10f + _ord * 36 + _timerRec * 3);
                        transform.rotation = Quaternion.Euler(0, 0,
                            _speed / 0.5f * 10f + _ord * 36f - Mathf.Sign(_speed) * 90f + _timerRec * 3);
                    }

                    break;
                case 2:
                    if (_speed <= 4f) _speed += 0.003f;
                    transform.position += Time.deltaTime * _speed * (Vector3)Calc.Degree2Direction(_ord * 30f);
                    transform.rotation = Quaternion.Euler(0, 0, _ord * 15f - Mathf.Sign(_speed) * 90f);
                    break;
                case 3:
                    if (_speed <= 0.5f + 0.5f * Mathf.Abs(_ord % 12 - 6)) _speed += 0.003f;
                    transform.position += Time.deltaTime * _speed * (Vector3)Calc.Degree2Direction(_ord * 5f);
                    break;
                case 4:
                    if (_speed <= 0.00001f) _speed += 0.00000001f;
                    else if( _speed <= 4f) _speed += 0.003f;
                    transform.position += Time.deltaTime * _speed * (Vector3)Calc.Degree2Direction(_ord * 60f);
                    transform.rotation = Quaternion.Euler(0, 0, _ord * 60f + _timer - Mathf.Sign(_speed) * 90f);
                    break;
                    
            }
            
            if(Vector3.Distance(GameManager.Manager.GetPlayerPos(),this.transform.position) >= 12f)
                EnemyBulletManager.Manager.EnemyBulletPool.Release(this);
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Player")) {
                GameManager.Manager.PlayerTakeDamage(1);
                EnemyBulletManager.Manager.EnemyBulletPool.Release(this);
            }
            
        }

        private void OnBecameInvisible() {
            //EnemyBulletManager.Manager.EnemyBulletPool.Release(this);
        }
    }
}
