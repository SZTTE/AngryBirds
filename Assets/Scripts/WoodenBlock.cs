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
        state.Full = 4;
        state.Now = 4;
    }

    protected override void Disappear()
    {
        SpecialEffectsManager.Instance.BlockPieces(_transform.position, p1, p2, p3);
        Destroy(gameObject);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        InitializeReferences();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
