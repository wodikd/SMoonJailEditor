using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public string musicName;
    public float Time 
    {
        get
        {
            return audioSource.time;
        }
        set
        {
            audioSource.time = Mathf.Clamp(value, 0, Length);
        }
    }
    public float Length
    {
        get => length;
    }

    public bool isPlaying
    {
        get => audioSource.isPlaying;
    }
    private AudioSource audioSource;
    private float length;

    private float testTime;


    private void Awake()
    {
        audioSource = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
        
        SetAudioClip(musicName);

        audioSource.Play();
        audioSource.Pause();
    }

    public void SwitchAudioPlay()
    {
        if (audioSource.isPlaying)
        {
            # if UNITY_EDITOR
            Debug.Log("Pause");
            # endif
            audioSource.Pause();
        }
        else
        {
            # if UNITY_EDITOR
            Debug.Log("UnPause");
            # endif
            audioSource.UnPause();
        }
    }

    public void SwitchAudioPlay(bool play)
    {
        if (play)
        {
            #if UNITY_EDITOR
            Debug.Log("UnPause");
            #endif
            audioSource.UnPause();
        }
        else
        {
            #if UNITY_EDITOR
            Debug.Log("Pause");
            #endif
            audioSource.Pause();
        }
    }

    public void SetAudioClip(string musicName)
    {
        audioSource.clip = Resources.Load<AudioClip>("Music/" + musicName);
        length = audioSource.clip.length;
    }
}