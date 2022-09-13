using UnityEngine;

namespace GameTool
{
    public static class ExtensionDebuger
    {

        public static void GameLog(this Debug debug, object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }

        //public static void Log(object message, UnityEngine.Object context)
        //{
        //    UnityEngine.Debug.Log(message.ToString(), context);
        //}

        //public static void LogFormat(string format, params object[] args)
        //{
        //    UnityEngine.Debug.LogFormat(format, args);
        //}

        public static void LogError(this Debug debug, object message)
        {
#if UNITY_EDITOR
            Debug.LogError(message.ToString());
#endif
        }

        public static void LogWarning(this Debug debug, object message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message.ToString());
#endif
        }
    }
}
