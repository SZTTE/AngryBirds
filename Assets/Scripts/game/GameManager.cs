using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip levelBGM;
    public static GameManager Instance;
    private bool _mute;
    private Tube _level;
    public int[] highestScore = {0};
    public Tube Level { get=>_level; }
    private AudioSource _audioSource;


    public void MuteSwitch()
    {
        _mute = !_mute;
        Debug.Log("mute is "+_mute);
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _level.Full = 5;
        _level.Now = 1;
        _mute = false;
        if (Instance == null)   Instance = this;
        else                    Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        _audioSource.clip = titleBGM;
        _audioSource.Play();
    }
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
        if (_audioSource.clip != titleBGM) _audioSource.clip = titleBGM;
    }

    public void GotoLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
        if (_audioSource.clip != titleBGM) _audioSource.clip = titleBGM;
    }

}
