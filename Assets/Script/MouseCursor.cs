using System;
using UnityEngine;
using UnityEditor;
using GameTool;
using SMoonJail.Editor;
using SMoonJail;
using UnityEngine.EventSystems;
using UnityEditor.Compilation;
using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using Unity.Collections;

public class MouseCursor
{
    public enum ECursorBehavior
    {
        None, Select, Bullet, Laser, Bomb
    }

    public static Action cursorBehavior;
    //public static CursorBehaviorDelegate CursorBehavior
    //{
    //    get
    //    {
    //        return cursorBehavior;
    //    }
    //    set
    //    {
    //        cursorBehavior = value;
    //    }
    //}

    public static Vector3 GetCursorPos(Camera camera)
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public static void SetCursorBehavior(ECursorBehavior eCursorBehavior)
    {
        switch (eCursorBehavior)
        {
            case ECursorBehavior.None:
                break;
            case ECursorBehavior.Select:
                SetCursorToSelect();
                break;
            case ECursorBehavior.Bullet:
                SetCursorToBullet();
                break;
            case ECursorBehavior.Laser:
                SetCursorToLaser();
                break;
            case ECursorBehavior.Bomb:
                Bomb();
                break;
            default:
                break;
        }

        void SetCursorToSelect()
        {
            Vector2 startPos = Vector2.zero;
            Vector2 endPos = Vector2.zero;

            cursorBehavior = () =>
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
#if UNITY_EDITOR
                        Debug.Log("pointer is over the gameobject");
#endif
                        return;
                    }

                    startPos = GameTool.Tool.WorldCursorPos(camera: GameManager.inGameCamera);

                    SetCursorToSelect2();
                }
            };

            void SetCursorToSelect2()
            {
                cursorBehavior = () =>
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        endPos = GameTool.Tool.WorldCursorPos(camera: GameManager.inGameCamera);

                        // 드래그인지 아닌지
                        if (startPos.Equals(endPos))
                        {
                            var ray = GameManager.inGameCamera.ScreenPointToRay(Input.mousePosition);
                            var rayHit2D = Physics2D.Raycast(
                                origin: ray.origin,
                                direction: ray.direction,
                                distance: 20
                                );

                            // 오브잭트를 클릭 했는지 안했는지
                            if (rayHit2D)
                            {
                                if (Input.GetKey(KeyCode.LeftControl))
                                {
                                    ObjectEditorManager.AddNodeToList(
                                        gameNode: rayHit2D.transform.GetComponent<GameNode>(),
                                        addMode: ListAddMode.addition
                                        );
                                }
                                else
                                {
                                    ObjectEditorManager.AddNodeToList(
                                        gameNode: rayHit2D.transform.GetComponent<GameNode>(),
                                        addMode: ListAddMode.beginning
                                        );
                                }


                            }
                            else
                            {
                                ObjectEditorManager.NodeList.Clear();
                            }




#if UNITY_EDITOR
                            Debug.Log($"Select Ray: {rayHit2D.transform?.name}");
#endif
                        }
                        else
                        {
#if UNITY_EDITOR
                            Debug.Log("Select drag");
#endif
                        }

                        SetCursorToSelect();
                    }
                };
            }
        }

        void SetCursorToBullet()
        {
            Vector2 startPos = Vector2.zero;
            Vector2 endPos = Vector2.zero;

            //Bullet node = null;

            cursorBehavior = () =>
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
#if UNITY_EDITOR
                        Debug.Log("pointer is over the gameobject");
#endif
                        return;
                    }

                    startPos = GameTool.Tool.WorldCursorPos(camera: GameManager.inGameCamera);

                    SetCursorToBullet2();
                }
            };

            void SetCursorToBullet2()
            {
                cursorBehavior = () =>
                {
                    var node = UnityEngine.Object.Instantiate(
                        original: GameManager.BulletPrefab,
                        position: startPos,
                        rotation: Quaternion.identity
                        ).GetComponent<Bullet>();

                    node.Set(
                        startPos: startPos,
                        angle: 0,
                        speed: 1,
                        time: GameManager.GameTime
                        );

                    ObjectEditorManager.AddNodeToList(gameNode: node, addMode: ListAddMode.beginning);

                    SetCursorToBullet3(bullet: node);
                };
            }

            void SetCursorToBullet3(Bullet bullet)
            {
                cursorBehavior = () =>
                {
                    bullet.Angle = GameTool.Tool.DirToAngle(bullet.StartPos - GameTool.Tool.WorldCursorPos(GameManager.inGameCamera));
                    ObjectEditorManager.UpdateNodeInfo(bullet.GetNodeType);

                    if (Input.GetMouseButtonUp(0))
                    {

                        SetCursorToBullet();
                    }
                };
            }

        }

        void SetCursorToLaser()
        {
            cursorBehavior = () =>
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        return;
                    }

                    SetCursorToLaser2();
                }
            };

            void SetCursorToLaser2()
            {
                cursorBehavior = () =>
                {
                    var node = UnityEngine.Object.Instantiate(
                        original: GameManager.LaserPrefab,
                        position: InputTool.WorldCursorPos(GameManager.inGameCamera),
                        rotation: Quaternion.identity
                        ).GetComponent<Laser>();

                    node.Set(
                        posAngle: 0,
                        angle: 0,
                        delayBeat: 16,
                        durationBeat: 4
                        );

                    SetCursorToLaser3(node);
                };
            }

            void SetCursorToLaser3(Laser node)
            {
                cursorBehavior = () =>
                {
                    float posAngle =
                        ExtensionMath.Vec2Deg(
                            InputTool.WorldCursorPos(GameManager.inGameCamera)
                            );
                    float angle =
                        ExtensionMath.Vec2Deg(
                            node.transform.position
                            );

                    node.Set(
                        posAngle: posAngle, 
                        angle: angle, 
                        delayBeat: 16, 
                        durationBeat: 4
                        );

                    node.area1.T = 1;
                    node.area2.T = 1;

                    if (!Input.GetMouseButton(0))
                    {
                        SetCursorToLaser4(node);
                    }
                };
            }
            
            void SetCursorToLaser4(Laser node)
            {
                cursorBehavior = () =>
                {
                    float angle =
                    ExtensionMath.Vec2Deg(
                        (Vector2)node.transform.position - InputTool.WorldCursorPos(GameManager.inGameCamera)
                        );
                    node.Angle = angle;  
                    if (Input.GetMouseButtonDown(0))
                    {
                        SetCursorToLaser();
                    }
                };
            }
        }

        void Bomb()
        {

        }
    }
}