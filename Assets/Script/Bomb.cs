using UnityEngine;

namespace SMoonJail
{
    class Bomb : GameNode
    {
        public int delayBeat;
        public int durationBeat;

        [HideInInspector]
        public Area area1;
        [HideInInspector]
        public Area area2;
        private void Awake()
        {
            area1 = Get("Area1");
            area2 = Get("Area2");

            Area Get(string name)
            {
                return transform.Find(name).GetComponent<Area>();
            }
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

        public void Set(Vector2 pos, int delayBeat, int durationBeat, float time)
        {
            Pos = pos;
            this.delayBeat = delayBeat;
            this.durationBeat = durationBeat;
            this.Time = time;

        }

        public Vector2 Pos
        {
            get
            {
                return Pos;
            }
            set
            {
                Pos = value;
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
                return 
                    durationBeat * Editor.MusicTool.GetBeatGap;
            }
        }

        public float ActionTime
        {
            get
            {
                return
                    delayBeat * Editor.MusicTool.GetBeatGap;
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
                return GameNodeType.Bomb;
            }
        }
    }
}