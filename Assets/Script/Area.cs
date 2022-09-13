using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMoonJail
{
    public class Area : MonoBehaviour
    {
        public float width = 1;
        private float t;
        public float T
        {
            get
            {
                return t;
            }
            set
            {
                transform.localScale = new(transform.localScale.x, t / width, 1);
                t = value;
            }
        }
    }
}

