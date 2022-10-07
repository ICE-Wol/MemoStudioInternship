using UnityEngine;

namespace _Scripts {
    public class Barrel : MonoBehaviour {
        private int health;

        private SpriteRenderer _spriteRenderer;

        [SerializeField] private Sprite[] _sprites;

        public void TakeDamage() {
            health -= 1;
        }
        // Start is called before the first frame update
        void Start() {
            health = 50;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update() {
            if (health < 25) _spriteRenderer.sprite = _sprites[1];
            if(health < 0) Destroy(this.gameObject);
        }
    }
}
