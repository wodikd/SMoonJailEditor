using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameTool;

namespace SMoonJail
{
    namespace Editor
    {
        public class TimeBar : MonoBehaviour, IDragHandler, IPointerClickHandler,
        IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
        {
            public bool isMouseEnter;
            public bool onDrag;
            public bool isClick;

            private const float dragSensitivity = 700;
            private const float scrollSensitivity = 0.5f;

            public static RectTransform rectTransform { get; private set; }
            public static GameObject timeBarImage { get; private set; }
            public static RectTransform timeHandle { get; private set; }
    
            private static RectTransform area;

            public float sizeX
            { 
                get
                {
                    return rectTransform.sizeDelta.x;
                }
                set
                {
                    //rectTransform.sizeDelta += Vector2.right * value;
                    rectTransform.sizeDelta = rectTransform.sizeDelta.x + value < 0 ? Vector2.right : rectTransform.sizeDelta + (value * Vector2.right);
                }
            }

            public float posX
            {
                get
                {
                    return rectTransform.pivot.x;
                }
                set
                {
                    rectTransform.pivot = (Vector2.right * Mathf.Clamp(value, 0, 1)) + (Vector2.up * 0.5f);
                }
            }

            // Start is called before the first frame update
            void Start()
            {
        
                rectTransform = 
                GameObject.Find("TimelineCanvas")
                .transform.Find("Timeline")
                .transform.Find("TimeBar")
                .transform.Find("Area").GetComponent<RectTransform>();

                timeBarImage = 
                GameObject.Find("TimelineCanvas")
                .transform.Find("Timeline")
                .transform.Find("TimeBar")
                .transform.Find("Area")
                .transform.Find("Image").gameObject;

                timeHandle =
                GameObject.Find("TimelineCanvas")
                .transform.Find("Timeline")
                .transform.Find("TimeBar")
                .transform.Find("Area")
                .transform.Find("Image")
                .transform.Find("Handle").GetComponent<RectTransform>();

                area = 
                GameObject.Find("TimelineCanvas")
                .transform.Find("Timeline")
                .transform.Find("TimeBar")
                .transform.Find("Area").GetComponent<RectTransform>();
            }

            private void Update()
            {
                if (isMouseEnter)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        sizeX = Input.GetAxisRaw("Mouse ScrollWheel") * dragSensitivity;
                    }
                    else
                    {
                        posX -= Input.GetAxisRaw("Mouse ScrollWheel") * scrollSensitivity;
                    }
                }
            }

            public void OnPointerClick(PointerEventData eventData)
            {

            }
            /* 
                * Todo: scale을 줄일 시 Handle이 안 움직이는 문제 해결
                * pos.x의 범위가 area의 position을 기준으로 정해서 생긴 문제
                * pos.x을 움직이는게 아닌 anchors를 움직여 조종하면 해결될 것으로 예상됨
                * 
                * 이전 Editor에서는 Handle이 고정되있고 뒤의 Image가 움직이는 형식이므로 참고할 수 없음
                */
            public void OnDrag(PointerEventData eventData)
            {
                // 마우스 우클릭시 취소
                if (Input.GetMouseButton(1))
                {
                    return;
                }

                onDrag = true;

                /*
                    * 마우스 위치를 크기의 백분율로 구한다
                    * 앵커를 그곳으로 이동시킨다
                    * 위치를 초기화한다
                    */

                float mousePosT = 
                    (timeBarImage.transform.InverseTransformPoint(Tool.WorldCursorPos(GameManager.timelineCamera)).x + area.rect.width / 2) / area.rect.width;

                float newTime = mousePosT < 0 ? 0
                    : mousePosT > 1 ? 1
                    : mousePosT;

                timeHandle.anchorMin = new Vector2(newTime, 0);
                timeHandle.anchorMax = new Vector2(newTime, 1);

                GameManager.GameTime01 = newTime;

                #region oldone
                //// Vector2 cursorPos = MouseCursor.GetCursorPos(GameManager.timelineCamera);
                //Vector2 pos = Vector2.right * timeBarImage.transform.InverseTransformPoint(MouseCursor.GetCursorPos(GameManager.timelineCamera));
                //// Vector2 pos = Vector2.right * MouseCursor.GetCursorPos(GameManager.timelineCamera);
                //Debug.Log(pos);
                //pos.x = Mathf.Clamp(pos.x, area.rect.width / -2, area.rect.width / 2);
                //// timeHandler.localPosition = pos;
                //timeHandler.localPosition = pos;
                // (Vector2.right * timeBarImage.transform.InverseTransformPoint(MouseCursor.GetCursorPos(GameManager.timelineCamera)), 0, area.rect.width);
                #endregion
            }

            public void SyncHandle()
            {
                timeHandle.anchorMin = new Vector2(GameManager.GameTime01, 0);
                timeHandle.anchorMax = new Vector2(GameManager.GameTime01, 1);
            }

            public void OnPointerEnter(PointerEventData eventData)
            {
                isMouseEnter = true;
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                isMouseEnter = false;
            }

            public void OnPointerDown(PointerEventData eventData)
            {
                isClick = true;
            }

            public void OnPointerUp(PointerEventData eventData)
            {
                isClick = false;
            }
        }
    }
}
