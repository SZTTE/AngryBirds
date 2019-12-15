using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButtonScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer stopPic = null;
    [SerializeField] private bool isPig = false;
    private void OnMouseDown()
    {
        if (!isPig)
        {
            GameManager.Instance.MuteSwitch();
        }
    }

    private void Update()
    {
        stopPic.enabled = GameManager.Instance.Mute;
    }
}
