using UnityEngine;

namespace _Scripts {
    public class EnemyA : Enemy.Enemy {
        private float posX;
    

        void Start() {
            Timer = 0;
            posX = transform.position.x;
        }

        // Update is called once per frame
        void Update() {
            transform.position = new Vector3(posX + Mathf.Sin(Timer / 5f * Mathf.Deg2Rad), transform.position.y);
            transform.position -= Vector3.up * 1f * Time.deltaTime;
            if (GameManager.Manager.GetPlayerPos().x > transform.position.x) {
                transform.localScale = new Vector3(-1f,1f,1f);
            }
            Timer++;
            AttackEvent();
            if (health <= 0) {
                DestroyEvent();
            }
        }

        public override void TakeDamage() {
            health -= 1;
        }

        protected override void AttackEvent() {
            if (Timer % 2400 == 0) {
                var dir = Vector2.SignedAngle(Vector2.right,
                    (Vector2)(GameManager.Manager.GetPlayerPos() - this.transform.position));
                for (int i = 1; i <= 10; i++) {
                    for (int j = 1; j <= i; j++) {
                        var b = EnemyBulletManager.Manager.EnemyBulletPool.Get();
                        b.SetInitials(1,1,5f - i/3f,dir + (j - i/2f) * 5f,transform.position);
                    }
                }
            }
        }

        protected override void DestroyEvent() {
            for (int i = 1; i <= 12; i++) {
                var b = EnemyBulletManager.Manager.EnemyBulletPool.Get();
                b.SetInitials(2,2,i,transform.position);
                Destroy(this.gameObject);
            }
        }
    }
}
