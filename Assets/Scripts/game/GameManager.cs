using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool _mute;
    private Tube _level;
    public int[] highestScore = {0};
    public Tube Level { get=>_level; }


    public void MuteSwitch()
    {
        _mute = !_mute;
        Debug.Log("mute is "+_mute);
    }

    void Start()
    {
        _level.Full = 5;
        _level.Now = 1;
        _mute = false;
        if (Instance == null)   Instance = this;
        else                    Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void GotoLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

}
