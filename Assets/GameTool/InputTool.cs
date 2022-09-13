using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{
    public static class InputTool
    {
        public enum InputType
        {

        }
        /// <summary>
        /// Get input mousePos
        /// </summary>
        public static Vector3 CursorPos
        {
            get
            {
                return Input.mousePosition;
            }
        }

        /// <summary>
        /// Get touch 0
        /// </summary>
        public static Touch TouchPos
        {
            get
            {
                return Input.GetTouch(0);
            }
        }

        /// <summary>
        /// Get cursorPos of camera
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static Vector2 WorldCursorPos(Camera camera) =>
            (Vector2)camera.ScreenToWorldPoint(CursorPos);

        //public static void InputKey(KeyCode key, System.Action action)
        //{
        //    if (Input.GetKeyDown(key))
        //    {
        //        action();
        //    }
        //}
    }
}
