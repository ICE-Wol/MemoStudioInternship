using System;
using UnityEngine;

namespace _Scripts {
    public class LevelTrigger : MonoBehaviour {
        [SerializeField] private int level;
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                GameManager.Manager.Level = level;
            }
        }
        
        
    }
}
