using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class PauseUIScript : MonoBehaviour
{
    private Animation _animation;
    private Canvas _canvas;
    [SerializeField]private SpriteRenderer stop = null;

    
    public void Out()
    {
        _canvas.enabled = true;
        _animation.Play("Out");
    }

    public void In()
    {
        _animation.Play("In");
        Invoke("CloseCanvas",0.5f);
    }

    private void CloseCanvas()
    {
        _canvas.enabled = false;
    }

    public void MuteSwitch()
    {
        GameManager.Instance.MuteSwitch();
        stop.enabled = GameManager.Instance.Mute;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    void Start()
    {
        _animation = GetComponent<Animation>();
        _canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        stop.enabled = GameManager.Instance.Mute;
    }
}
