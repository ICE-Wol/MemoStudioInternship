using UnityEngine;

namespace _Scripts {
    public class AidPack : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Player")) {
                GameManager.Manager.PlayerTakeDamage(-10);
                Destroy(this.gameObject);
            }
        }
    }
}
