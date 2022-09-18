using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

namespace SMoonJail.Editor
{
    public class LaserEditor : NodeEditor 
    {
        private static InputField posAngleInputField;
        private static InputField timeInputField;
        private static InputField delayBeatInputField;
        private static InputField durationBeatField;
        private static InputField angleInputField;

        private void Awake()
        {
            posAngleInputField = Get("PosAngleInputField");
            timeInputField = Get("TimeInputField");
            delayBeatInputField = Get("DelayBeatInputField");
            durationBeatField = Get("DurationBeatInputField");
            angleInputField = Get("AngleInputField");

            InputField Get(string name)
            {
                return
                    transform
                    .Find(name)
                    .GetComponent<InputField>();
            }
        }

        public override void UpdateNodeInfo()
        {
            const string replaceString = "--";

            bool equalPosAngle = true;
            bool equalTime = true;
            bool equalDelay = true;
            bool equalDuration = true;
            bool equalAngle = true;

            var nodeList = ObjectEditorManager.NodeList.ConvertAll(gameNode =>
            {
                return gameNode.GetComponent<Laser>();
            });

            var refNode = nodeList[0];
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (equalPosAngle && (refNode.PosAngle != nodeList[i].PosAngle))
                {
                    equalPosAngle = false;
                }

                if (equalTime && (refNode.Time != nodeList[i].Time))
                {
                    equalTime = false;
                }

                if (equalDelay && (refNode.delayBeat != nodeList[i].delayBeat))
                {
                    equalDelay = false;
                }
                
                if (equalDuration && (refNode.durationBeat != nodeList[i].durationBeat))
                {
                    equalDuration = false;
                }

                if (equalAngle && (refNode.Angle != nodeList[i].Angle))
                {
                    equalAngle = false;
                }
            }

            for (int i = 0; i < nodeList.Count; i++)
            {
                if (equalPosAngle)
                {
                    posAngleInputField.text =
                        refNode.PosAngle.ToString();
                }
                else
                {
                    posAngleInputField.text =
                        replaceString;
                }

                if (equalTime)
                {
                    angleInputField.text =
                        refNode.Time.ToString();
                }
                else
                {
                    angleInputField.text =
                        replaceString;
                }

                if (equalDelay)
                {
                    delayBeatInputField.text =
                        refNode.delayBeat.ToString();
                }
                else
                {
                    delayBeatInputField.text =
                        replaceString;
                }

                if (equalDuration)
                {
                    delayBeatInputField.text =
                        refNode.durationBeat.ToString();
                }
                else
                {
                    delayBeatInputField.text =
                        replaceString;
                }

                if (equalAngle)
                {
                    angleInputField.text =
                        refNode.Angle.ToString();
                }
                else
                {
                    angleInputField.text =
                        replaceString;
                }
            }
        }

        public void SetPosAngleForInputField(InputField inputField)
        {
            var posAngle = float.Parse(inputField.text);

            var nodeList = ObjectEditorManager.NodeList.ConvertAll(node =>
            {
                return node.GetComponent<Laser>();
            });
            for (int i = 0; i < nodeList.Count; i++)
            {
                nodeList[i].PosAngle = posAngle;
            }
            UpdateNodeInfo();
        }

        public void SetTimeForInputField(InputField inputField)
        {
            var time = float.Parse(inputField.text);

            var nodeList = ObjectEditorManager.NodeList.ConvertAll(node =>
            {
                return node.GetComponent<Laser>();
            });
            for (int i = 0; i < nodeList.Count; i++)
            {
                nodeList[i].Time = time;
            }
            UpdateNodeInfo();
        }

        public void SetDelayForInputField(InputField inputField)
        {
            var delay = int.Parse(inputField.text);

            var nodeList = ObjectEditorManager.NodeList.ConvertAll(node =>
            {
                return node.GetComponent<Laser>();
            });

            for (int i = 0; i < nodeList.Count; i++)
            {
                nodeList[i].delayBeat = delay;
            }
            UpdateNodeInfo();
        }

        public void SetDurationForInputField(InputField inputField)
        {
            var duration = int.Parse(inputField.text);

            var nodeList = ObjectEditorManager.NodeList.ConvertAll(node =>
            {
                return node.GetComponent<Laser>();
            });

            for (int i = 0; i < nodeList.Count; i++)
            {
                nodeList[i].durationBeat = duration;
            }
            UpdateNodeInfo();
        }

        public void SetAngleForInputField(InputField inputField)
        {
            var angle = float.Parse(inputField.text);

            var nodeList = ObjectEditorManager.NodeList.ConvertAll(node =>
            {
                return node.GetComponent<Laser>();
            });

            for (int i = 0; i < nodeList.Count; i++)
            {
                nodeList[i].Angle = angle;
            }
            UpdateNodeInfo();
        }
    }
}
