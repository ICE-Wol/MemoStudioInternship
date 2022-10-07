using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts {
    public class EnemyBulletManager : MonoBehaviour {
        [SerializeField] private EnemyBullet enemyBullet;
        [SerializeField] private Sprite[] bulletSprites;
        public ObjectPool<EnemyBullet> EnemyBulletPool;

        public static  EnemyBulletManager Manager;

        public Sprite GetBulletSprite(int num) => bulletSprites[num];

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }
        private void Start() {
            EnemyBulletPool = new ObjectPool<EnemyBullet>(() => {
                return Instantiate(enemyBullet);
            }, bullet => {
                bullet.gameObject.SetActive(true);
            }, bullet => {
                bullet.gameObject.SetActive(false);
            }, bullet => {
                Destroy(bullet.gameObject);
            }, false, 200, 1000);
        }
    }
}