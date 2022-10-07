using System;
using _Scripts.Function;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

namespace _Scripts {
    public class PlayerBulletManager : MonoBehaviour {
        [SerializeField] private PlayerBullet playerBullet;
        public ObjectPool<PlayerBullet> PlayerBulletPool;

        public static PlayerBulletManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }
        private void Start() {
            PlayerBulletPool = new ObjectPool<PlayerBullet>(() => {
                return Instantiate(playerBullet);
            }, bullet => {
                bullet.gameObject.SetActive(true);
            }, bullet => {
                bullet.gameObject.SetActive(false);
            }, bullet => {
                Destroy(bullet.gameObject);
            }, false, 200, 400);
        }
        
        public void BarbetteFire(Vector3 position, float direction, float radius) {
            position.y += radius * Mathf.Cos(Mathf.Deg2Rad * direction);
            position.x -= radius * Mathf.Sin(Mathf.Deg2Rad * direction);
            position = Calc.RandomRange(position, 0.2f);
            //var temp = Instantiate(playerBullet, position, Quaternion.Euler(0, 0, direction));
            var temp = PlayerBulletManager.Manager.PlayerBulletPool.Get();
            temp.SetProperties(position, Quaternion.Euler(0, 0, direction), direction);
        }

        public void PlayerFire(Vector3 position, Vector3 direction, float radius) {
            var dir2 = new Vector3(direction.y, -direction.x, 0);
            position -= dir2 * radius;
            position = Calc.RandomRange(position, 0.2f);
            var temp = PlayerBulletManager.Manager.PlayerBulletPool.Get();
            var dir = Vector2.SignedAngle(Vector2.right, direction);
            //Debug.Log(direction);
            //important: Signed Angle start with one.
            temp.SetProperties(position, Quaternion.Euler(0, 0, dir), dir);
        }
        
        public void PlayerFire(Vector3 position, Vector3 direction, float radius, float speed) {
            var dir2 = new Vector3(direction.y, -direction.x, 0);
            position -= dir2 * radius;
            var temp = PlayerBulletManager.Manager.PlayerBulletPool.Get();
            var dir = Vector2.SignedAngle(Vector2.right, direction);
            //Debug.Log(direction);
            //important: Signed Angle start with one.
            temp.SetProperties(position, Quaternion.Euler(0, 0, dir), dir, speed);
        }
    }
}