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
    public bool Mute => _mute;

    public Tube _level;
    public int[] highestScore;
    public Tube Level { get=>_level; }
    private AudioSource _audioSource;
    private AudioListener _audioListener;
    private bool _atLevel;
    public int[] levelStars;

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
        _level.Full = 24;
        _level.Now = 1;
        _mute = false;
        DontDestroyOnLoad(gameObject);
        _audioSource.clip = titleBGM;
        _audioSource.Play();
        _atLevel = false;
        levelStars = new int[50];
        //最后再读档
        ReadPlayerPref();
    }
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
        if (_atLevel)
        {
            _audioSource.Stop();
            _audioSource.clip = titleBGM;
            _audioSource.Play();
            _audioSource.loop = true;
            _atLevel = false;
        }
        WritePlayerPref();
    }
    public void GotoLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
        if (_atLevel)
        {
            _audioSource.Stop();
            _audioSource.clip = titleBGM;
            _audioSource.Play();
            _audioSource.loop = true;
            _atLevel = false;
        }
        WritePlayerPref();
    }
    public void GotoLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
        if (!_atLevel)
        {
            _audioSource.Stop();
            _audioSource.clip = levelBGM;
            _audioSource.Play();
            _audioSource.loop = true;
            _atLevel = true;
        }
        WritePlayerPref();
    }

    private void WritePlayerPref()
    {
        //存關卡分數
        for (int i = 0; i <= _level.Full; i++)
        {
            PlayerPrefs.SetInt("ScoreOfLevel"+i,highestScore[i]);
        }
        //存星星
        for (int i = 0; i <= _level.Full; i++)
        {
            PlayerPrefs.SetInt("StarsOfLevel"+i,levelStars[i]);
        }
        //存當前關卡
        PlayerPrefs.SetInt("LevelNow",_level.Now);
    }
    private void ReadPlayerPref()
    {
        //读關卡分數
        for (int i = 0; i <= _level.Full; i++)
        {
            highestScore[i] = PlayerPrefs.GetInt("ScoreOfLevel"+i,0);
        }
        //读星星
        for (int i = 0; i <= _level.Full; i++)
        {
            levelStars[i] = PlayerPrefs.GetInt("StarsOfLevel"+i,0);
        }
        //读當前關卡
        _level.Now=PlayerPrefs.GetInt("LevelNow",1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayBeginAnimation()
    {
        SceneManager.LoadScene("BeginAnimation");
        _audioSource.Pause();
    }

    public void PlayEndingAnimation()
    {
        SceneManager.LoadScene("EndingAnimation");
        _audioSource.Pause();
    }
}
