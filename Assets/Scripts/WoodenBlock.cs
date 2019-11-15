using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBlock : Block
{
    public WoodenBlock()
    {
        BirdSensitivity.Yellow = 1.5f;
    }

    protected override void Disappear()
    {
        //待完成
    }
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(BirdSensitivity.Red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
