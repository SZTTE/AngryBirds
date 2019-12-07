using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderScript : MonoBehaviour
{
    public bool draging = false;
    public MonoBehaviour slingScript;
    [SerializeField] private AudioClip stretchSound;


    private void OnMouseDown()
    {
        draging = true;
        AudioManager.Instance.Play(stretchSound,1);
    }

    private void OnMouseUp()
    {
        draging = false;
    }
}
