using UnityEngine;
using GameTool;

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

            if (gameTime < ActionTime + Time)
            {
                float runTime = Time - gameTime;
                float t = runTime / ActionTime;

                area1.T = 1;
                area2.T = t;
            }
            else
            {
                float t = 1 - ((gameTime - (ActionTime + Time)) / DurationTime);
                area1.T = t;
                area2.T = t;
            }
        }


        public void Set(float posAngle, float angle, int delayBeat, int durationBeat, float time)
        {
            Angle = angle;
            PosAngle = posAngle;
            this.delayBeat = delayBeat;
            this.durationBeat = durationBeat;
            this.Time = time;
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

                transform.rotation = 
                    Quaternion.AngleAxis(Angle, transform.forward);
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

                transform.position = ExtensionMath.Deg2Vec(PosAngle) * 15;
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

        public float DurationTime
        {
            get
            {
                return durationBeat * Editor.MusicTool.GetBeatGap;
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