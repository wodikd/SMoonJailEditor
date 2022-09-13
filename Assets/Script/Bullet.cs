using UnityEngine;

namespace SMoonJail
{
    public class Bullet : GameNode
    {
        private float speed;
        private float angle;
        private Vector2 startPos;

        private new Rigidbody2D rigidbody;

        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
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
            }
        }
        public Vector2 StartPos
        {
            get
            {
                return startPos;
            }
            set
            {
                startPos = value;
            }
        }

        public Bullet(float time, Vector2 startPos, float angle, float speed)
        {
            Set(time, startPos, angle, speed);
        }

        public void Awake()
        {
            //((IGameNode)this).UpdateNode();

            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Set(float time, Vector2 startPos, float angle, float speed)
        {
            this.Time = time;
            this.startPos = startPos;
            this.angle = angle;
            this.speed = speed;

            UpdateAll();
        }

        public override void UpdateValue()
        {
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }
        public override void UpdatePosition()
        {
            rigidbody.MovePosition(startPos + ((GameManager.GameTime - Time) * -speed * GameManager.mapInfo.BPM) * (Vector2)transform.right);
        }

        #region Legacy
        //void IGameNode.UpdateNode()
        //{
        //    UpdateNode();
        //}

        //void IGameNode.UpdatePosition()
        //{
        //    UpdatePosition();
        //}



        //public void Set(Vector2 startPos)
        //{
        //    this.startPos = startPos;
        //    UpdateNode();
        //}

        //public void Set(Vector2 startPos, float angle)
        //{
        //    this.startPos = startPos;
        //    this.angle = angle;

        //    UpdateNode();
        //}

        //public void Set(Vector2 startPos, float angle, float speed)
        //{
        //    this.startPos = startPos;
        //    this.angle = angle;
        //    this.speed = speed;

        //    UpdateNode();
        //}

        //public NodeType GetNodeType // It works
        //{
        //    get
        //    {
        //        return NodeType.Bullet;
        //    }
        //}

        #endregion

        public override GameNodeType GetNodeType
        {
            get
            {
                return GameNodeType.Bullet;
            }
        }
    }
}
