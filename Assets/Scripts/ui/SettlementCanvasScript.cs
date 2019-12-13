using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using Image = UnityEngine.UIElements.Image;

public class SettlementCanvasScript : MonoBehaviour
{
    public static SettlementCanvasScript Instance;
    [SerializeField] private int score2Stars = 0;
    [SerializeField] private int score3Stars = 0;
    [SerializeField] private UnityEngine.UI.Image[] star = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField]private Text highestScoreText = null;
    [SerializeField] private UnityEngine.UI.Image stamp1 = null;//新纪录印章
    [SerializeField] private UnityEngine.UI.Image stamp2 = null;

    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Appear(int score)
    {
        if (GameManager.Instance.Level.Now <= LevelManagerScript.Instance.level)
            GameManager.Instance._level.Now = LevelManagerScript.Instance.level + 1;
        Time.timeScale = 0;
        int level = LevelManagerScript.Instance.level;
        if (score > GameManager.Instance.highestScore[level])
            GameManager.Instance.highestScore[level] = score;
        else
        {
            stamp1.enabled = false;
            stamp2.enabled = false;
        }
        scoreText.text = score.ToString();
        highestScoreText.text = GameManager.Instance.highestScore[level].ToString();
        if (score < score2Stars)
        {
            star[2].enabled = false;
            star[1].enabled = false;
        }
        else if (score < score3Stars)
            star[2].enabled = false;
        GetComponent<Canvas>().enabled = true;
    }
}
