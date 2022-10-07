using TMPro;
using UnityEngine;

namespace _Scripts {
    public struct PlayerValue {
        public int NumLife;
        public int NumGrenade;
        public int WeaponType;

        public PlayerValue(int life,int gren,int type) {
            NumLife = life;
            NumGrenade = gren;
            WeaponType = type;
        }
    }
    public class UIController : MonoBehaviour {
        [SerializeField] private BodyController body;
        [SerializeField] private TMP_Text textDisplay;
        private PlayerValue _value;

        // Update is called once per frame
        void Update() {
            _value = body.GetPlayerValue();
            string text = "Health: " + _value.NumLife + "\n\n" +
                          "Grenade: " + _value.NumGrenade + "\n\n" +
                          "Weapon: " + _value.WeaponType + "\n\n";
            textDisplay.text = text;
        }
    }
}
