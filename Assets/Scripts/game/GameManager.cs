using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip titleBGM = null;
    [SerializeField] private AudioClip levelBGM = null;
    public static GameManager Instance;
    private bool _mute;
    private Tube _level;
    public int[] highestScore = {0};
    public Tube Level { get=>_level; }
    private AudioSource _audioSource;
    private AudioListener _audioListener;
    private bool _atLevel;

    void Awake()
    {
        if (Instance == null)   Instance = this;
        else                    Destroy(gameObject);
    }

    public void MuteSwitch()
    {
        _mute = !_mute;
        if (_mute) AudioListener.volume = 0;
        else AudioListener.volume = 1;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioListener = GetComponent<AudioListener>();
        _level.Full = 5;
        _level.Now = 1;
        _mute = false;
        DontDestroyOnLoad(gameObject);
        _audioSource.clip = titleBGM;
        _audioSource.Play();
        _atLevel = false;
    }
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
        if (_atLevel)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(titleBGM);
            _atLevel = false;
        }
    }
    public void GotoLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
        if (_atLevel)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(titleBGM);
            _atLevel = false;
        }
    }
    public void GotoLevel(int levelIndex)
    {
        Debug.Log("Level" + levelIndex);
        SceneManager.LoadScene("Level" + levelIndex);
        if (!_atLevel)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(levelBGM);
            _atLevel = true;
        }
    }

}
