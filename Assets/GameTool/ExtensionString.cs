using UnityEngine;

namespace GameTool
{
    public static class ExtensionString
    {
        public static void Log(this string str)
        {
#if UNITY_EDITOR
            Debug.Log(str);
#endif
        }

        public static void LogError(this string str)
        {
#if UNITY_EDITOR
            Debug.LogError(str);
#endif
        }
        public static void LogWarning(this string str)
        {
#if UNITY_EDITOR
            Debug.LogWarning(str);
#endif
        }
    }
}
