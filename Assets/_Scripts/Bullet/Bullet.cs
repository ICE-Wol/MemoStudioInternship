using UnityEngine;

namespace _Scripts.Bullet
{
    public class Bullet : MonoBehaviour
    {
        private BulletData _bulletData;
        private SpriteRenderer _spriteRenderer;
        private void Awake() {
            _bulletData = gameObject.AddComponent<BulletData>();
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void RefreshSprite() {
            Color color = _bulletData.GetBulletColorInRGB();
            color.a = _bulletData.alpha;
            _spriteRenderer.color = color;
        }

        private void RefreshTransform() {
            Transform temp = this.transform;
            temp.rotation = Quaternion.Euler(_bulletData.rotation);
            temp.localScale = _bulletData.scale;
            temp.position = _bulletData.position;
        }
        private void Update() {
            RefreshSprite();
            RefreshTransform();
        }
    }
}
