using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMoonJail.Editor
{
    public class MusicTool
    {
        private float getBeatGap;
        public static float GetBeatGap
        {
            get
            {
                var value1 = GameManager.mapInfo.BPM / 60f;
                var value2 = (4 / GameManager.mapInfo.Beat) / value1;

                return value2;
            }
        }

        [UnityEditor.MenuItem("GameEditor/BPM")]
        private static void GetMusicInfo()
        {
            System.Text.StringBuilder stringBuilder = new();

            stringBuilder
                .AppendLine("GetBeatCount: " + GetBeatCount.ToString())
                ;

            GameTool.Debugger.Log(GetBeatCount);
        }

        public static float GetBeatCount
        {
            get
            {
                var value = 4 / GameManager.mapInfo.Beat;
                return GameManager.mapInfo.BPM / value;
            }
        }
    }
}