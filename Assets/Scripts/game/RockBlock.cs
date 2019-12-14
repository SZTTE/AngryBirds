using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlock : Block
{
    public RockBlock()
    {
        BirdSensitivity.Black = 5f;
        BirdSensitivity.White = 2f;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeReferences();
        InitializeValue();
    }
}
