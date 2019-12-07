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
}
