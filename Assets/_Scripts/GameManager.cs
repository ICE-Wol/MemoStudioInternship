using UnityEditor;
using UnityEngine;

namespace _Scripts {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Manager;
        public int Level { set; get; }
        

        [SerializeField] private GameObject[] init; 
        
        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }
        
        void Start() {
            Level = 0;
        }
        
        void Update()
        {
            switch (Level) {
                default:
                    break;
                case 1:
                    foreach (var obj in init) {
                        //obj.transform.position += Vector3.right * 5f * Time.deltaTime;
                        
                    }
                    break;
            }
        }
    }
}
