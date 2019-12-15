using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderScript : MonoBehaviour
{
    public bool draging = false;
    public MonoBehaviour slingScript;


    private void OnMouseDown()
    {
        if (Time.timeScale != 0)
        {
            draging = true;
        }
    }

    private void OnMouseUp()
    {
        draging = false;
    }

    public void End()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
