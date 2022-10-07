using UnityEngine;

namespace _Scripts {
    public class Landmine : MonoBehaviour
    {
        public void Explode() {
            for (int i = 1; i <= 72; i++) {
                var b = EnemyBulletManager.Manager.EnemyBulletPool.Get();
                b.SetInitials(3,3,i,this.transform.position);
            }
            Destroy(this.gameObject);
        }
        private void OnCollisionEnter2D(Collision2D c) {
            //Debug.Log("coll");
            var col = c.collider;
            if (col.CompareTag("Player") || col.CompareTag("PlayerBullet")) {
                for (int i = 1; i <= 72; i++) {
                    var b = EnemyBulletManager.Manager.EnemyBulletPool.Get();
                    b.SetInitials(3,3,i,this.transform.position);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
