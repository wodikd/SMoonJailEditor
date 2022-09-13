using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace GameTool
{
    public static class ExtensionMath
    {
        #region vector2, degree, radian
        /// <summary>
        /// vector2 to degree
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float Vec2Deg(float y, float x) =>
             Mathf.Atan2(y, x) * 180.0f / Mathf.PI;

        /// <summary>
        /// vecotr2 to degree
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float Vec2Deg(Vector2 vector) =>
            Mathf.Atan2(vector.y, vector.x) * 180.0f / Mathf.PI;

        /// <summary>
        /// vector2 to radian
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float Vec2Rad(float y, float x) =>
             Mathf.Atan2(y, x);

        /// <summary>
        /// vector2 to radian
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float Vec2Rad(Vector2 vector) =>
            Mathf.Atan2(vector.y, vector.x);

        /// <summary>
        /// radian to degree
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static float Rad2Deg(float radian) =>
            radian * 180.0f / Mathf.PI;

        /// <summary>
        /// degree to radian
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static float Deg2Rad(float degree) =>
            (Mathf.PI / 180.0f) * degree;

        /// <summary>
        /// radian to vector2
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static Vector2 Rad2Vec(float radian) =>
            new(Mathf.Cos(radian), Mathf.Sin(radian));

        /// <summary>
        /// degree to vector2
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Vector2 Deg2Vec(float degree) =>
            Rad2Vec(degree * Mathf.Deg2Rad);
        #endregion

        /// <summary>
        /// Set vector random value
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public static void Random(this Vector2 vector, float minValue, float maxValue) =>
            vector.Set(UnityEngine.Random.Range(minValue, maxValue), UnityEngine.Random.Range(minValue, maxValue));

        /// <summary>
        /// Custom Round
        /// </summary>
        /// <param name="num">value</param>
        /// <param name="digit">The higher the number, the higher the rounding.</param>
        /// <returns></returns>
        public static int Round(float num, int digit)
        {
            var temp = Math.Pow(10.0, digit);
            return
                (int)(Math.Round(num * temp) / temp);
        }
    }
}
