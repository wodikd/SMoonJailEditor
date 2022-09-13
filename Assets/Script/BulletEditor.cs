using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SMoonJail
{
    namespace Editor
    {
        [System.Serializable]
        public class BulletEditor : MonoBehaviour, INodeEditor
        {
            private static InputField startPosXInputField;
            private static InputField startPosYInputField;
            private static InputField timeInputField;
            private static InputField speedInputField;
            private static InputField angleInputField;

            private void Awake()
            {
                startPosXInputField = transform.Find("PosXInputField").GetComponent<InputField>();
                startPosYInputField = transform.Find("PosYInputField").GetComponent<InputField>();
                timeInputField = transform.Find("TimeInputField").GetComponent<InputField>();
                speedInputField = transform.Find("SpeedInputField").GetComponent<InputField>();
                angleInputField = transform.Find("AngleInputField").GetComponent<InputField>();
            }

            public void UpdateNodeInfo()
            {
                const string replaceString = "--";

                bool equalTime = true;
                bool equalSpeed = true;
                bool equalPosX = true;
                bool equalPosY = true;
                bool equalAngle = true;

                var bulletList = ObjectEditorManager.NodeList.ConvertAll((gameNode) =>
                {
                    return gameNode.GetComponent<Bullet>();
                });

                for (int i = 0; i < bulletList.Count; i++)
                {
                    if (equalTime && (bulletList[0].Time != bulletList[i].Time))
                    {
                        equalTime = false;
                    }

                    if (equalSpeed && (bulletList[0].Speed != bulletList[i].Speed))
                    {
                        equalSpeed = false;
                    }

                    if (equalPosX && (bulletList[0].StartPos.x != bulletList[i].StartPos.x))
                    {
                        equalPosX = false;
                    }

                    if (equalPosY && (bulletList[0].StartPos.y != bulletList[i].StartPos.y))
                    {
                        equalPosY = false;
                    }

                    if (equalAngle && (bulletList[0].Angle != bulletList[i].Angle))
                    {
                        equalAngle = false;
                    }
                }

                for (int i = 0; i < bulletList.Count; i++)
                {
                    if (equalTime)
                    {
                        timeInputField.text = bulletList[0].Time.ToString();
                    }
                    else
                    {
                        timeInputField.text = replaceString;
                    }

                    if (equalPosX)
                    {
                        startPosXInputField.text = bulletList[0].StartPos.x.ToString();
                    }
                    else
                    {
                        startPosXInputField.text = replaceString;
                    }

                    if (equalPosY)
                    {
                        startPosYInputField.text = bulletList[0].StartPos.y.ToString();
                    }
                    else
                    {
                        startPosYInputField.text = replaceString;
                    }

                    if (equalSpeed)
                    {
                        speedInputField.text = bulletList[0].Speed.ToString();
                    }
                    else
                    {
                        speedInputField.text = replaceString;
                    }

                    if (equalAngle)
                    {
                        angleInputField.text = bulletList[0].Angle.ToString();
                    }
                    else
                    {
                         angleInputField.text = replaceString;
                    }
                }
            }

            public void SetStartPosXForInputField(InputField inputField)
            {
                var posX = float.Parse(inputField.text);

                for (int i = 0; i < ObjectEditorManager.NodeList.Count; i++)
                {
                    var node = ObjectEditorManager.NodeList[i].GetComponent<Bullet>();
                    node.StartPos = new Vector2(posX, node.StartPos.y);
                    node.UpdateAll();
                    UpdateNodeInfo();
                }
            }

            public void SetStartPosYForInputField(InputField inputField)
            {
                var posY = float.Parse(inputField.text);

                for (int i = 0; i < ObjectEditorManager.NodeList.Count; i++)
                {
                    var node = ObjectEditorManager.NodeList[i].GetComponent<Bullet>();
                    node.StartPos = new Vector2(node.StartPos.x, posY);
                    node.UpdateAll();
                    UpdateNodeInfo();
                }
            }

            public void SetTimeForInputField(InputField inputField)
            {
                var time = float.Parse(inputField.text);

                for (int i = 0; i < ObjectEditorManager.NodeList.Count; i++)
                {
                    var node = ObjectEditorManager.NodeList[i].GetComponent<Bullet>();
                    node.Time = time;
                    node.UpdateAll();
                    UpdateNodeInfo();
                }
            }

            public void SetSpeedForInputField(InputField inputField)
            {
                var speed = float.Parse(inputField.text);

                for (int i = 0; i < ObjectEditorManager.NodeList.Count; i++)
                {
                    var node = ObjectEditorManager.NodeList[i].GetComponent<Bullet>();
                    node.Speed = speed;
                    node.UpdateAll();
                    UpdateNodeInfo();
                }
            }

            public void SetAngleForInputField(InputField inputField)
            {
                var angle = float.Parse(inputField.text);

                for (int i = 0; i < ObjectEditorManager.NodeList.Count; i++)
                {
                    var node = ObjectEditorManager.NodeList[i].GetComponent<Bullet>();
                    node.Angle = angle;
                    node.UpdateAll();
                    UpdateNodeInfo();
                }
            }
        }
    }
}
