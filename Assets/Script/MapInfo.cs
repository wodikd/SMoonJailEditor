
namespace SMoonJail
{
    [System.Serializable]
    public class MapInfo
    {
        public string name;
        public string musicName;
        public string difficulty;
        public float BPM;
        [UnityEngine.SerializeField]
        private float beat;

        public float Beat
        {
            get
            {
                return beat;
            }
            set
            {
                beat = value;
            }
        }
    }
}