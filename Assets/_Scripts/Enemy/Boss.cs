using System;
using _Scripts.Function;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Enemy {
    public class Boss : Enemy {
        private Vector3 nxtPos;

        private void Update() {
            transform.position = Calc.Approach(transform.position, nxtPos, 64f * Vector3.one);
            if (Calc.Equal(transform.position, nxtPos, 0.01f)) {
                float dir = Random.Range(20f, 160f);
                float dis = Random.Range(5f, 6f);
                nxtPos = GameManager.Manager.GetPlayerPos() + dis * (Vector3)Calc.Degree2Direction(dir);
                for (int i = 1; i <= 12; i++) {
                    var b = EnemyBulletManager.Manager.EnemyBulletPool.Get();
                    b.SetInitials(2, 2, i, transform.position);
                }
            }

            if (nxtPos.x > transform.position.x) {
                transform.localScale = new Vector3(-2f, 2f, 2f);
                
            }
            else {
                transform.localScale = new Vector3(2f, 2f, 2f);
            }

            Timer++;
            if(Timer % 20 == 0) AttackEvent();

            if (health < 0) {
                Destroy(this.gameObject);
            }
        }

        protected override void AttackEvent() {
            for (int i = 1; i <= 6; i++) {
                var b = EnemyBulletManager.Manager.EnemyBulletPool.Get();
                b.SetInitials(4, 4, i, transform.position);
            }
        }

        protected override void DestroyEvent() { }

        public override void TakeDamage() {
            health -= 10;
        }
    }
}
