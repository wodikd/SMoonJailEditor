# if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEditorStartSetting
{
    public static bool isOn = false;

    private const string startScene = "InGame";
    // Start is called before the first frame update
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        if (!isOn)
        {
            return;
        }
        // Debug.Log($"{startScene} Init Scene --------------");
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.CompareTo(startScene) != 0)
        {
            Debug.Log($"{UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}은(는) 시작 씬이 아니므로 {startScene}을 로드합니다");
            UnityEngine.SceneManagement.SceneManager.LoadScene(startScene);
        }
    }
}
# endif