using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBlock : Block
{
    public WoodenBlock()
    {
        BirdSensitivity.Yellow = 4f;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeReferences();
        InitializeValue();
    }
}
