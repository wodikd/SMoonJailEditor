using UnityEngine;

namespace SMoonJail
{
    class Bomb : GameNode
    {
        public override void UpdatePosition()
        {
            
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