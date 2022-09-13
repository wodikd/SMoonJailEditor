using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SMoonJail
{
    namespace Editor
    {
        public class NodeListPanel : MonoBehaviour
        {
            public void SetCursorNoneForToggle(Toggle toggle)
            {
                if (toggle.isOn)
                {
                    MouseCursor.SetCursorBehavior(eCursorBehavior: MouseCursor.ECursorBehavior.None);
                }
                else
                {
                    Debug.Log("NoneToggle is false");
                }
            }

            public void SetCursorSelectForToggle(Toggle toggle)
            {
                if (toggle.isOn)
                {
                    MouseCursor.SetCursorBehavior(eCursorBehavior: MouseCursor.ECursorBehavior.Select);

                }
                else
                {
                    Debug.Log("SelectToggle is false");
                }
            }

            public void SetCursorBulletForToggle(Toggle toggle)
            {
                if (toggle.isOn)
                {
                    MouseCursor.SetCursorBehavior(eCursorBehavior: MouseCursor.ECursorBehavior.Bullet);

                }
                else
                {
                    Debug.Log("BulletToggle is false");
                }
            }

            public void SetCursorLaserForToggle(Toggle toggle)
            {
                if (toggle.isOn)
                {
                    MouseCursor.SetCursorBehavior(eCursorBehavior: MouseCursor.ECursorBehavior.Laser);

                }
                else
                {
                    Debug.Log("LaserToggle is false");
                }
            }
            public void SetCursorBombForToggle(Toggle toggle)
            {
                if (toggle.isOn)
                {
                    MouseCursor.SetCursorBehavior(eCursorBehavior: MouseCursor.ECursorBehavior.Bomb);

                }
                else
                {
                    Debug.Log("BombToggle is false");
                }
            }
        }

    }
}