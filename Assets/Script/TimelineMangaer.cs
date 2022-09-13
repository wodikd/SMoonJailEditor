using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SMoonJail
{
    namespace Editor
    {
        public class TimelineMangaer : MonoBehaviour
        {
            private static TimeBar timeBar;
            private static InputField timeInputField { get; set; }

            // static public void SetTime
            // {
            //     set => 
            // }

            private void Awake()
            {
                timeBar = FindObjectOfType<TimeBar>();

                timeInputField =
                GameObject.Find("TimelineCanvas")
                .transform.Find("TimePanel")
                .transform.Find("TimeInputField").GetComponent<InputField>();

                // timeBar = 
                // GameObject.Find("TimelineCanvas")
                // .transform.Find("Timeline")
                // .transform.Find("TimeBar").GetComponent<TimeBar>();
            }

            public static void UpdateTimeline()
            {
                float time = GameManager.GameTime;

                //timeText.text = $"{time % 86400}:{time % 3600}:{time % 60}:{time - System.Math.Truncate(time)}";

                float timeDecimal = time - Truncate(time);

                timeInputField.text = $"{Truncate(time / 3600):00}:{Truncate(time % 60):00}:{Truncate(timeDecimal * 10000):0000}";
                timeBar.SyncHandle();

                float Truncate(float value)
                {
                    return (float)System.Math.Truncate(value);
                }
            }

            public void SetTimeForInputField(InputField inputField)
            {
                string timeText = inputField.text;
                string[] timeTextArray = timeText.Split(':',' ');
                if (timeTextArray.Length >= 1)
                {
                    if (float.TryParse(timeTextArray[0], out float time))
                    {
                        GameManager.GameTime = time;
                    }
                    # if UNITY_EDITOR
                    else
                    {
                        Debug.LogError($"{timeTextArray[0]} is not right format");
                    }
                    # endif
                }
                else
                {
                    //switch (timeTextArray.Length)
                    //{
                    //    case 2:
                    //        float newTime = 0;
                    //        if (float.TryParse(timeTextArray[1], out float millisecond))
                    //        {
                    //            newTime += millisecond * 0.0001f;
                    //        };
                    //        if (float.TryParse(timeTextArray[0], out float second))
                    //        {
                    //            newTime += second;
                    //        }
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }
        }
    }
}
