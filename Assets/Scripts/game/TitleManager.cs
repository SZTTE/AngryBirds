using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;
    // Start is called before the first frame update
    
    public void GoToTitleScene()
    {
        GameManager.Instance.GoToTitleScene();
    }
    public void GotoLevelSelectScene()
    {
        GameManager.Instance.GotoLevelSelectScene();
    }
    public void GotoLevel(int levelIndex)
    {
        GameManager.Instance.GotoLevel(levelIndex);
    }
    void Start()
    {
        Instance = this;
    }
    public void MuteSwitch()
    {
        GameManager.Instance.MuteSwitch();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
    }
    public void NextLevel()
    {
        GameManager.Instance.GotoLevel(LevelManagerScript.Instance.level+1);
    }

    public void Restart()
    {
        GameManager.Instance.GotoLevel(LevelManagerScript.Instance.level);
    }

    public void Exit()
    {
        GameManager.Instance.Exit();
    }
}
