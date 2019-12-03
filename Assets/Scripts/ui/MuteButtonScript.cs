using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButtonScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer stopPic;
    [SerializeField] private bool isPig;
    private void OnMouseDown()
    {
        if(!isPig)
            GameManager.Instance.MuteSwitch();
        stopPic.enabled = !stopPic.enabled;
    }
}
