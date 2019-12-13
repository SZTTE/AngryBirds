using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerScript : MonoBehaviour
{
    public static LevelManagerScript Instance;
    private float _lastTimePigExist;
    private float _lastTimeBirdExist;
    private float _lastTimeBlockMove;
    private int _score = 0;
    private AudioSource _audioSource;
    public int Score => _score;
    public int level = 0;
    [SerializeField] private Text scoreBox = null;
    [SerializeField] private AudioClip startSound = null;
    [SerializeField] private AudioClip winSound = null;
    [SerializeField] private AudioClip loseSound = null;
    /// <summary>
    /// 加分
    /// </summary>
    /// <param name="scoreToAdd">要加多少分？</param>
    public void ScoreAdd(int scoreToAdd)
    {
        _score += scoreToAdd;
        scoreBox.text = _score.ToString();
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

    private bool _settled = false;//胜负已分

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    
        _lastTimePigExist = Time.time;
        _lastTimeBirdExist = Time.time;
        _lastTimeBlockMove = Time.time;
        _score = 0;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(startSound);
    }
    void Update()
    {
        
        if (!_settled)
        {
            //Debug.Log(_lastTimeBlockMove+","+Time.time);
            if (Time.time - _lastTimePigExist > 0.5f) //下面写胜利的代码
                if(Time.time - _lastTimeBlockMove > 3 || Time.time - _lastTimePigExist > 10f)
                {
                    StartCoroutine(SlingScript.Instance.ShowBirdsScore());
                    _settled = true;
                    _audioSource.PlayOneShot(winSound);
                }
            else if (Time.time -_lastTimeBirdExist > 1f && Time.time - _lastTimeBlockMove > 3)//下面写失败的代码
            {
                FailureUIScript.Instance.Appear();
                _settled = true;
                _audioSource.PlayOneShot(loseSound);
            }
        }

        //Debug.Log("time="+Time.time+",_Block="+_lastTimeBlockMove);
    }
}
