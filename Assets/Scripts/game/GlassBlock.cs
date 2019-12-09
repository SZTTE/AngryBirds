using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBlock : Block
{
    public GlassBlock()
    {
        BirdSensitivity.Blue = 7f;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeReferences();
        InitializeValue();
    }
}
