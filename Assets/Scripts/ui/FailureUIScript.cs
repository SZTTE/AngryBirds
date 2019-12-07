using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class FailureUIScript : MonoBehaviour
{
    private Canvas _canvas;
    public static FailureUIScript Instance;

    public void Appear()
    {
        _canvas.enabled = true;
        Time.timeScale = 0;
    }

    private void Start()
    {
        Instance = this;
        _canvas = GetComponent<Canvas>();
    }
}
