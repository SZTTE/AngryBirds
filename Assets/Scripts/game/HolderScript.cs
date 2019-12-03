using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderScript : MonoBehaviour
{
    public bool draging = false;
    public MonoBehaviour slingScript;
    //private tran
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        draging = true;
    }

    private void OnMouseUp()
    {
        draging = false;
    }
}
