﻿using UnityEngine;

namespace _Scripts.Function {
    public class Calc : MonoBehaviour {
        //if too big, the following traces will be a rectangle.
        private static float _epsilon = 0.001f;

        public static bool Equal(float argument1, float argument2) {
            return Mathf.Abs(argument1 - argument2) <= _epsilon;
        }
        public static bool Equal(float argument1, float argument2, float epsilon) {
            return Mathf.Abs(argument1 - argument2) <= epsilon;
        }
        
        public static bool Equal(Vector3 argument1, Vector3 argument2) {
            return Equal(argument1.x, argument2.x) &&
                   Equal(argument1.y, argument2.y) &&
                   Equal(argument1.z, argument2.z);
        }
        public static bool Equal(Vector3 argument1, Vector3 argument2, float epsilon) {
            return Equal(argument1.x, argument2.x, epsilon) &&
                   Equal(argument1.y, argument2.y, epsilon) &&
                   Equal(argument1.z, argument2.z, epsilon);
        }

        public static Vector2 Degree2Direction(float degree) {
            return new Vector2(Mathf.Cos(Mathf.Deg2Rad * degree), Mathf.Sin(Mathf.Deg2Rad * degree));
        }
        
        
        /// <summary>
        /// A function which approach the current value to the target value, the closer the slower.
        /// </summary>
        /// <param name="current">Value type, the current value which is approaching the target value.</param>
        /// <param name="target">the final destination of the approach process.</param>
        /// <param name="rate">the rate of approach process, the bigger the slower.</param>
        /// <returns></returns>
        public static float Approach(float current, float target, float rate) {
            if (Mathf.Abs(current - target) >= _epsilon) {
                current -= (current - target) / rate;
            }
            else {
                current = target;
            }

            return current;
        }
        
        public static Vector3 Approach(Vector3 current, Vector3 target, Vector3 rate) {
            current.x = Approach(current.x, target.x, rate.x);
            current.y = Approach(current.y, target.y, rate.y);
            current.z = Approach(current.z, target.z, rate.z);
            return current;
        }

        public static Vector3 RandomRange(Vector3 vec,float range) {
            vec.x += Random.Range(-range, range);
            vec.y += Random.Range(-range, range);
            vec.z += Random.Range(-range, range);
            return vec;
        }
    }
}