using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SMoonJail
{
    namespace Editor
    {
        public enum ListAddMode
        {
            beginning = 0,
            addition = 1
        }


        [System.Serializable]
        public class ObjectEditorManager : MonoBehaviour
        {
            private static List<GameNode> nodeList = new List<GameNode>();
            private static NodeEditor bulletEditor;
            private static NodeEditor laserEditor;

            private static NodeEditor currentEditor;
            public static NodeEditor CurrentEditor
            {
                get
                {
                    return currentEditor;
                }
            }
            private void Awake()
            {
                bulletEditor = FindObjectOfType<BulletEditor>();
                laserEditor = FindObjectOfType<LaserEditor>();

            }

            public static void AddNodeToList(GameNode gameNode, ListAddMode addMode)
            {
                if (nodeList.Count == 0)
                {
                    nodeList.Add(gameNode);

                    UpdateNodeInfo();
                }
                else if (nodeList[0].GetNodeType == gameNode.GetNodeType)
                {
                    switch (addMode)
                    {
                        case ListAddMode.beginning:
                            nodeList.Clear();
                            nodeList.Add(gameNode);
                            break;
                        case ListAddMode.addition:
                            nodeList.Add(gameNode);
                            break;
                        default:
                            break;
                    }

                    SwitchNodeEditor();

                    UpdateNodeInfo();
                }
                else
                {
                    GameTool.Debugger.Log($"type of list[0] is {nodeList[0].GetNodeType} but you tried to add {gameNode.GetNodeType}");
                }

                
            }

            private static void SwitchNodeEditor()
            {
                bulletEditor.gameObject.SetActive(false);
                laserEditor.gameObject.SetActive(false);

                switch (NodeList[0].GetNodeType)
                {
                    case GameNodeType.None:
                        break;
                    case GameNodeType.Bullet:
                        currentEditor = bulletEditor;
                        break;
                    case GameNodeType.Laser:
                        currentEditor = laserEditor;
                        break;
                    case GameNodeType.Bomb:
                        break;
                    default:
                        break;
                }

                currentEditor.gameObject.SetActive(true);
            }

            private static void SwitchNodeEditor(GameNodeType nodeType)
            {
                bulletEditor.gameObject.SetActive(false);
                laserEditor.gameObject.SetActive(false);

                switch (nodeType)
                {
                    case GameNodeType.None:
                        break;
                    case GameNodeType.Bullet:
                        currentEditor = bulletEditor;
                        break;
                    case GameNodeType.Laser:
                        currentEditor = laserEditor;
                        break;
                    case GameNodeType.Bomb:
                        break;
                    default:
                        break;
                }

                currentEditor.gameObject.SetActive(true);
            }

            /// <summary>
            /// == currentEditor.UpdateNodeInfo()
            /// </summary>
            public static void UpdateNodeInfo()
            {
                currentEditor.UpdateNodeInfo();
            }

            //public static void UpdateNodeInfo(GameNodeType nodeType)
            //{
            //    switch (nodeType)
            //    {
            //        case GameNodeType.None:
            //            break;
            //        case GameNodeType.Bullet:
            //            bulletEditor.UpdateNodeInfo();
            //            break;
            //        case GameNodeType.Laser:
            //            laserEditor.UpdateNodeInfo();
            //            break;
            //        case GameNodeType.Bomb:
            //            break;
            //        default:
            //            break;
            //    }
            //}

            public static List<GameNode> NodeList
            {
                get
                {
                    return nodeList;
                }
            }
        }
    }
}