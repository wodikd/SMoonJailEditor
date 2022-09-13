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

            UpdateAll();
        }

        protected virtual void OnEnable()
        {
            UpdateAll();
        }

        public abstract void UpdateValue();
        public abstract void UpdatePosition();
        public virtual void UpdateAll()
        {
            UpdateValue();
            UpdatePosition();
        }

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
