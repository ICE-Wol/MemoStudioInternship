using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Laser {
    public class LaserManager : MonoBehaviour
    {
        [SerializeField] private LaserUnit laserUnit;
        public ObjectPool<LaserUnit> LaserPool;

        public static LaserManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }
        
        
        private void Start() {
            LaserPool = new ObjectPool<LaserUnit>(() => {
                return Instantiate(laserUnit);
            }, unit => {
                unit.gameObject.SetActive(true);
            }, unit => {
                unit.gameObject.SetActive(false);
            }, unit => {
                Destroy(unit.gameObject);
            }, false, 100, 1000);
        }
    }
}
