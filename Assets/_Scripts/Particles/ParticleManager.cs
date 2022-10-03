using System;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Particles {
    public class ParticleManager : MonoBehaviour
    {
        [SerializeField] private Particle particle;
        public ObjectPool<Particle> ParticlePool;

        public static ParticleManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }
        
        private void Start() {
            ParticlePool = new ObjectPool<Particle>(() => {
                return Instantiate(particle);
            }, p => {
                p.gameObject.SetActive(true);
            }, p => {
                p.gameObject.SetActive(false);
            }, p => {
                Destroy(p.gameObject);
            }, false, 200, 400);
        }
        
    }
}
