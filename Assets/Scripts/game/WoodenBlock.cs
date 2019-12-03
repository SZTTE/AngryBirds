using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBlock : Block
{
    public WoodenBlock()
    {
        BirdSensitivity.Yellow = 1.5f;
        life.Full = 50;
        life.Now = 50;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeReferences();
        InitializeValue();
    }
}
