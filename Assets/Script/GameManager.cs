using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using SMoonJail.Editor;
using SMoonJail;
using GameTool;

// using UnityScene = UnityEngine.SceneManagement;
//Todo:
//Hack:
//Undone:
public class GameManager : MonoBehaviour
{
    public Gradient themeGradient;
    public MapInfo _mapInfo;
    public string dataPath = @"/Resources/Test/GameNodeList.txt";

    public static MapInfo mapInfo;
    public static MouseCursor mouseCursor;

    public static Camera inGameCamera;
    public static Camera listCamera;
    public static Camera timelineCamera;
    public static Camera objectEditorCamera;

    public static AudioManager audioManager;

    public static TimelineMangaer timelineManager;
    public static ObjectEditorManager objectEditorManager;

    private static GameNode bulletPrefab;
    public static GameNode BulletPrefab { get => bulletPrefab; }

    public static List<GameNode> gameNodeList = new List<GameNode>();

    private static bool isOnUI;

    private static GameManager gameManager;
    private static SaveLoadManager SLManager;

    // Start is called before the first frame update
    void Awake()
    {
        // DontDestroyOnLoad(gameObject);

        mapInfo = _mapInfo;

        gameManager = GetComponent<GameManager>();
        audioManager = gameObject.GetComponent<AudioManager>();
        SLManager = new(GameNodeList);

        bulletPrefab = Resources.Load<GameNode>("Prefab/Node/Bullet");

        LoadSceneAdditive("List");
        LoadSceneAdditive("Timeline");
        LoadSceneAdditive("ObjectEditor");

        StartCoroutine(LateAwake());

        MouseCursor.SetCursorBehavior(MouseCursor.ECursorBehavior.Select);

        //Vector2 a = Vector2.zero;
        //Vector2 b = Vector2.zero;

        //System.Diagnostics.Stopwatch stopwatch1 = new System.Diagnostics.Stopwatch();
        //System.Diagnostics.Stopwatch stopwatch2 = new System.Diagnostics.Stopwatch();

        //stopwatch1.Start();
        //for (ulong i = 0; i < 1000_0000; i++)
        //{
        //    var c = a == b;
        //}
        //stopwatch1.Stop();

        //stopwatch2.Start();
        //for (ulong i = 0; i < 1000_0000; i++)
        //{
        //    var c = a.Equals(b);
        //}
        //stopwatch2.Stop();

        //Debug.Log($"==: {stopwatch1.Elapsed}, equals: {stopwatch2.Elapsed}");

    }

    void Start()
    {
        timelineManager = FindObjectOfType<TimelineMangaer>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.SwitchAudioPlay();
            UpdateGame();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Save");

            StartCoroutine(
                SLManager.Save(
                path: Application.dataPath + dataPath
                )
            );

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Load");

            DeleteNode();

            StartCoroutine(
                SLManager.Load(
                path: Application.dataPath + dataPath
                )
            );

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Clear");

            DeleteNode();
        }
        if (!isOnUI)
        {
            
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DeleteNode(ObjectEditorManager.NodeList);
        }
        

        if (audioManager.isPlaying)
        {
            UpdateGame();
        }
        else
        {

        }

        //[UnityEditor.MenuItem()]
        if (Input.GetKeyDown(KeyCode.B))
        {
            var value1 = mapInfo.BPM / 60f;
            var value2 = 0.25f / value1;

            //Debuger.Log($"{value1}, {value2}, {value2 * 620}");
            //float GetBeatCount(float BPM, int beat)
            //{
            //    var value = 4 / beat;
            //    return BPM / value;
            //}
        }

        MouseCursor.cursorBehavior();
    }

    public static void UpdateGame()
    {
        UpdateNodes();
        TimelineMangaer.UpdateTimeline();
    }
    
    /// <summary>
    /// 노트 업데이트
    /// </summary>
    public static void UpdateNodes()
    {
        for (int i = 0; i < GameNodeList.Count; i++)
        {
            GameNodeList[i].UpdatePosition();
        }
    }

    public static void DeleteNode()
    {
        for (int i = 0; i < gameNodeList.Count; i++)
        {
            Destroy(gameNodeList[i].gameObject);
        }
        gameNodeList.Clear();
    }

    public static void DeleteNode(List<GameNode> nodeList)
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            for (int j = 0; j < gameNodeList.Count; j++)
            {
                if (gameNodeList[j].Equals(nodeList[j]))
                {
                    gameNodeList.RemoveAt(j);
                }
            }

            Destroy(nodeList[i].gameObject);
        }
    }

    private void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void RegulateViewport(Rect rect)
    {
        listCamera.rect = new Rect(0, rect.y, rect.x, rect.height);
        objectEditorCamera.rect = new Rect(rect.x + rect.width, rect.y, 1 - rect.width, rect.height);
        timelineCamera.rect = new Rect(0, 0, 1, 1 - rect.height);
    }
    private IEnumerator LateAwake()
    {
        while (true)
        {
            yield return null;

            bool inGameCameraOn = GameObject.Find("InGameCamera").TryGetComponent<Camera>(out inGameCamera);
            bool listCameraOn = GameObject.Find("ListCamera").TryGetComponent<Camera>(out listCamera);
            bool timeLineCameraOn = GameObject.Find("TimelineCamera").TryGetComponent<Camera>(out timelineCamera);
            bool objectEditorCameraOn = GameObject.Find("ObjectEditorCamera").TryGetComponent<Camera>(out objectEditorCamera);

            if (inGameCameraOn && listCameraOn && timeLineCameraOn && objectEditorCameraOn)
            {
                RegulateViewport(inGameCamera.rect);

                StartCoroutine(MoveCamera());

                break;
            }
        }
    }

    private IEnumerator MoveCamera()
    {
        var oldPos = InputTool.WorldCursorPos(inGameCamera);

        while (true)
        {
            if (!isOnUI)
            {
                while (Input.GetMouseButton(2))
                {
                    var newPos = InputTool.WorldCursorPos(inGameCamera);

                    var deltaPos = oldPos - newPos;

                    inGameCamera.transform.position += new Vector3(deltaPos.x, deltaPos.y, -10);

                    inGameCamera.transform.position = Vector3.ClampMagnitude(inGameCamera.transform.position, 50);

                    yield return null;
                }

                inGameCamera.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel") * 10;

                oldPos = InputTool.WorldCursorPos(inGameCamera);
            }

            yield return null;
        }
        
    }

    public static GameManager GetManager
    {
        get
        {
            return gameManager;
        }
    }

    /// <summary>
    /// return time / length
    /// </summary>
    public static float GameTime01
    {
        get
        {
            return audioManager.Time / audioManager.Length;
        }
        set
        {
            audioManager.Time = audioManager.Length * Mathf.Clamp(value, 0, 1);

            UpdateGame();
        }
    }

    /// <summary>
    /// return time
    /// </summary>
    public static float GameTime
    {
        get
        {
            return audioManager.Time;
        }
        set
        {
            audioManager.Time = Mathf.Clamp(value, 0, audioManager.Length);

            UpdateGame();
        }
    }

    public static List<GameNode> GameNodeList
    {
        get
        {
            return gameNodeList;
        }
    }

    // public void AdditiveScene(eSCENE_TYPE _sceneType)
    // {
    //     this.SetState(eSCENE_MANAGER_STATE.LOAD);

    //     string sceneName = this.GetTypeToString(_sceneType);
    //     this.m_CurrentScene.StartCoroutine(LoadUnityScene(sceneName, UnityScene.LoadSceneMode.Additive));
    // }

    // private IEnumerator LoadUnityScene(string _name, UnityScene.LoadSceneMode mode)
    // {
    //     this.SetState(eSCENE_MANAGER_STATE.LOAD);
    //     UnityScene.SceneManager.LoadScene(_name, mode);
    //     yield return new WaitForSeconds(0.5f);
    //     UnityScene.Scene loadScene = UnityScene.SceneManager.GetSceneByName(_name);

    //     if (null == this.m_DicSubScene)
    //     {
    //         this.m_DicSubScene = new Dictionary<string, UnityScene.Scene>();
    //     }

    //     this.m_DicSubScene.Add(_name, loadScene);
    //     Logger.LogFormat("[SceneManager] AddtiveScene : {0}", UnityScene.SceneManager.GetActiveScene().name);
    //     this.SetState(eSCENE_MANAGER_STATE.LOAD_END);
    // }
    // private IEnumerator LoadUnitySceneAsync(string _sceneName, UnityScene.LoadSceneMode mode)
    // {
    //     yield return UnityScene.SceneManager.LoadSceneAsync(_sceneName, mode);
    // }
    // private IEnumerator UnLoadUnitySceneAsync(string _sceneName)
    // {
    //     this.m_DicSubScene.Remove(_sceneName);
    //     yield return UnityScene.SceneManager.UnloadSceneAsync(_sceneName);
    // }
}
