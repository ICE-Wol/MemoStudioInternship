using UnityEditor;
using UnityEngine;

namespace _Scripts {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Manager;
        public int Level { set; get; }
        [SerializeField] private GameObject[] init;
        [SerializeField] private GameObject[] enemy;
        [SerializeField] private BodyController player;

        private int _timer;
        private bool _haveBoss;

        public void PlayerTakeDamage(int damage) => player.TakeDamage(damage);
        public Vector3 GetPlayerPos() => player.transform.position;
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
            _timer = 0;
            _haveBoss = false;
        }
        
        void Update()
        {
            switch (Level) {
                default:
                    break;
                case 1:
                    _timer++;
                    foreach (var obj in init) {
                        obj.transform.position += Vector3.right * 1f * Time.deltaTime;
                    }
                    if (_timer % 2000 == 0) {
                        var pos = player.transform.position + Vector3.right * Random.Range(-2f, 9f) + Vector3.up * 10f;
                        Instantiate(enemy[1], pos,Quaternion.Euler(0,0,0));
                    }
                    if (_timer % 1800 == 0) {
                        var pos = player.transform.position + Vector3.right * Random.Range(-2f, 9f) + Vector3.up * 10f;
                        Instantiate(enemy[0], pos,Quaternion.Euler(0,0,0));
                    }
                    
                    if (_timer % 3000 == 0) {
                        var pos = player.transform.position + Vector3.right * Random.Range(-2f, 9f) + Vector3.up * 10f;
                        Instantiate(enemy[2], pos,Quaternion.Euler(0,0,0));
                    }
                    break;
                case 2:
                    _timer++;
                    if (!_haveBoss) {
                        Instantiate(enemy[5], player.transform.position + Vector3.up * 6f,Quaternion.Euler(0,0,0));
                        _haveBoss = true;
                    }
                    if (_timer % 2000 == 0) {
                        var pos = player.transform.position + Vector3.right * Random.Range(9f,11f);
                        Instantiate(enemy[3], pos,Quaternion.Euler(0,0,0));
                        pos = player.transform.position + Vector3.right * Random.Range(9f,11f);
                        Instantiate(enemy[4], pos,Quaternion.Euler(0,0,0));
                    }
                    if (_timer % 2300 == 0) {
                        var pos = player.transform.position + Vector3.right * Random.Range(9f,11f);
                        Instantiate(enemy[4], pos,Quaternion.Euler(0,0,0));
                    }
                    if (_timer % 1800 == 0) {
                        var pos = player.transform.position + Vector3.right * Random.Range(-2f, 9f) + Vector3.up * 10f;
                        Instantiate(enemy[0], pos,Quaternion.Euler(0,0,0));
                    }
                    break;
            }
        }
    }
}
