using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{
    public class Tool
    {
        private void Test()
        {

            //Camera.main.screen
        }

        public static float DirToAngle(Vector2 dir)
        {
            return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        //public Vector2 ScreenPointToDirection(Camera camera)
        //{
        //    return (Vector2)camera.ScreenToWorldPoint(Input.mousePosition).normalized;
        //}

        //public static float ScreenPointToAngle(Camera camera, Vector2 vector2)
        //{
        //    var cursorDir = vector2 - (Vector2)camera.ScreenToWorldPoint(Input.mousePosition).normalized;
        //    return DirToAngle(cursorDir);
        //}

        public static Vector2 WorldCursorPos(Camera camera)
        {
            return (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

}