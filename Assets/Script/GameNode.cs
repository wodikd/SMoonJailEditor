using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMoonJail
{
    public abstract class GameNode : MonoBehaviour
    {
        public bool targetPlayer;
        public bool randomPosition;
        public abstract GameNodeType GetNodeType { get; }

        protected virtual void Start()
        {
            GameManager.gameNodeList.Add(this);

            UpdatePosition();
        }

        protected virtual void OnEnable()
        {
            UpdatePosition();
        }

        public abstract void UpdatePosition();
        

        [SerializeField]
        private float time;
        public float Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
    }
}
