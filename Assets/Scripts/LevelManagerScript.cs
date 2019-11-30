using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    public static LevelManagerScript Instance;
    private float _lastTimePigExist;
    private float _lastTimeBirdExist;
    private float _lastTimeBlockMove;

    public LevelManagerScript()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 汇报猪还活着，要是长时间没有汇报，就认为猪死光了，那么就赢了
    /// </summary>
    public void PigExist()
    {
        _lastTimePigExist = Time.time;
    }
    
    /// <summary>
    /// 汇报鸟还活着，要是长时间没有汇报，就认为鸟死光了，那么就输了
    /// </summary>
    public void BirdExist()
    {
        _lastTimeBirdExist = Time.time;
    }

    public void BlockMove()
    {
        _lastTimeBlockMove = Time.time;
    }

    void Update()
    {
        if (Time.time - _lastTimeBlockMove > 3)        //在块都静止之后才判定胜负
        {
            if (Time.time - _lastTimePigExist > 0)     //下面写胜利的代码
            {
                Debug.Log("You Win");
            }
            else if (Time.time -_lastTimeBirdExist > 3)//下面写失败的代码
            {
                Debug.Log("You Lose");
            }
        }
    }
}
