using UnityEngine;
using UnityEngine.Serialization;


//1. the bullet cant be spawned near the player
//2. some invisible bullet is used as a spawner
//3. some bullet doesnt have check radius.
//(cant simply make it zero,because it would be checked as a point.
namespace _Scripts
{
    public class BulletData : MonoBehaviour {
        /// <summary>
        /// Whether the bullet can collide with the player.
        /// </summary>
        public bool canCollide;

        /// <summary>
        /// The type of the bullet.
        /// None zero means types in definitions.
        /// Zero means special types.
        /// </summary>
        public int type;
        
        /// <summary>
        /// The default radius of the bullet associated with its type.
        /// TODO: maybe put this in manager.
        /// </summary>
        public int[] defaultRadius;
        
        /// <summary>
        /// The collision radius of the bullet.
        /// Normally it is strictly associated with the type of the bullet.
        /// </summary>
        public int radius;

        /// <summary>
        /// The alpha channel of the bullet.
        /// </summary>
        public float alpha;

        /// <summary>
        /// The color of the bullet in HSV.
        /// In most cases, Only the first value, that is the hue,
        /// will have the necessity to be changed.
        /// </summary>
        public Vector3 colorInHSV;

        public Color GetBulletColorInRGB()
            => Color.HSVToRGB(colorInHSV.x,colorInHSV.y,colorInHSV.z);

        /// <summary>
        /// The position of the bullet,
        /// only the first two dimension is valid.
        /// </summary>
        public Vector3 position;
        
        /// <summary>
        /// The rotation of the bullet,
        /// only the first two dimension is valid.
        /// </summary>
        public Vector3 rotation;
        
        /// <summary>
        /// The scale of the bullet,
        /// only the first two dimension is valid.
        /// </summary>
        public Vector3 scale;
    }
}
