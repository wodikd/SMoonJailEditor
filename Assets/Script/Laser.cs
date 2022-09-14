using UnityEngine;
using System;
using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEditor.Compilation;
using UnityEngine.UIElements;
using Unity.VisualScripting.FullSerializer;
using SMoonJail.Editor;

namespace SMoonJail
{
    class Laser : GameNode
    {
        public int delayBeat;
        public int durationBeat;

        private float angle;
        private float posAngle;

        [HideInInspector]
        public Area area1;
        [HideInInspector]
        public Area area2;

        private void Awake()
        {
            area1 = transform.Find("Area1").GetComponent<Area>();
            area2 = transform.Find("Area2").GetComponent<Area>();
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void UpdatePosition()
        {
            if (!IsAction)
            {
                area1.T = 0;
                area2.T = 0;

                return;
            }

            var gameTime = GameManager.GameTime;

            if (Time < gameTime)
            {
                float runTime = gameTime - Time;
                float t = runTime / ActionTime;

                area1.T = 1;
                area2.T = t;
            }
            else
            {
                    
            }
        }

        public override void UpdateValue()
        {
           
        }

        public void Set(float time, float posAngle, float angle, int delayBeat, int durationBeat)
        {
            Time = time;
            Angle = angle;
            PosAngle = posAngle;
            this.delayBeat = delayBeat;
            this.durationBeat = durationBeat;
        }
        
        public new float Time
        {
            get
            {
                return base.Time;
            }
            set
            {
                float time = value;
                float gap = MusicTool.GetBeatGap;

                time = (time / gap) * gap;

                base.Time = time;
            }
        }

        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;

                transform.rotation = Quaternion.
                    AngleAxis(angle, transform.forward);
            }
        }

        public float PosAngle
        {
            get
            {
                return posAngle;
            }
            set
            {
                posAngle = value;

                transform.position = GameTool.ExtensionMath
                    .Deg2Vec(posAngle) * 15;
                    
            }
        }

        public float EndTime
        {
            get
            {
                return (delayBeat + durationBeat)
                    * Editor.MusicTool.GetBeatGap;
            }
        }

        public float ActionTime
        {
            get
            {
                return delayBeat * Editor.MusicTool.GetBeatGap;
            }
        }

        public bool IsAction
        {
            get
            {
                var gameTime = GameManager.GameTime;

                if (gameTime < Time || gameTime > EndTime + Time)
                {
                    return false;
                }

                return true;
            }
        }

        public override GameNodeType GetNodeType
        {
            get
            {
                return GameNodeType.Laser;
            }
        }
    }
}