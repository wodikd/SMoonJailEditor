using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace SMoonJail
{
    namespace Editor
    {
        public class NodeBuilder
        {
            public float time;
            public Vector2 startPos;
            public float angle;
            public float speed;

            public const int delay = 200;

            public NodeBuilder()
            {
                time = 0;
                startPos = Vector2.zero;
                angle = 0;
                speed = 0;
            }

            public NodeBuilder SetTime(float time)
            {
                this.time = time;

                return this;
            }

            public NodeBuilder SetStartPos(Vector2 startPos)
            {
                this.startPos = startPos;

                return this;
            }

            public NodeBuilder SetAngle(float angle)
            {
                this.angle = angle;

                return this;
            }

            public NodeBuilder SetSpeed(float speed)
            {
                this.speed = speed;

                return this;
            }

            public Bullet BuildBullet()
            {

                var bullet = Object.Instantiate(
                    original: GameManager.BulletPrefab
                    ).GetComponent<Bullet>();

                bullet.Set(
                    time: time,
                    startPos: startPos,
                    angle: angle,
                    speed: speed
                    );

                return bullet;

                //Bullet(
                //    time: time,
                //    startPos: startPos,
                //    angle: angle,
                //    speed: speed
                //    );

            }
        }
    }
}