using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentPage : MonoBehaviour
{
    private void Awake()
    {
        if(GameManager.Instance!=null) Destroy(gameObject);
    }

    public void MusicStart()
    {
        GameManager.Instance.MusicPlay();
    }
}
