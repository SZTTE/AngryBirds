using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBlock : Block
{
    public WoodenBlock()
    {
        BirdSensitivity.Yellow = 1.5f;
        life.Full = 50000;
        life.Now = 50000;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeReferences();
        InitializeValue();
    }
}
