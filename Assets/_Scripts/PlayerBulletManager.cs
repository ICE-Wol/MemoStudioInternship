using System;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts {
    public class PlayerBulletManager : MonoBehaviour {
        [SerializeField] private PlayerBullet playerBullet;
        public ObjectPool<PlayerBullet> Pool;

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
            Pool = new ObjectPool<PlayerBullet>(() => {
                return Instantiate(playerBullet);
            }, bullet => {
                bullet.gameObject.SetActive(true);
            }, bullet => {
                bullet.gameObject.SetActive(false);
            }, bullet => {
                Destroy(bullet.gameObject);
            }, false, 200, 400);
        }
    }
}