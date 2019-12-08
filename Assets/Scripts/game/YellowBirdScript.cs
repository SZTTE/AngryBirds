﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBirdScript : Bird
{
    public YellowBirdScript()
    {
        life.Full = 1000;
        life.Now = 1000;
        state.Full = 2;//鸟最健康的时候在第二状态
        state.Now = 2;
    }

    public override void Skill()
    {
        Debug.Log("here");
        _rigidbody2D.velocity *= 2;
    }

    public void Smoke()//由动画启动
    {
        SpecialEffectsManager.Instance.Smoke(_transform.position); //这边为什么可以赋给一个vec2的值？
    }

    protected override void Disappear()
    {
        
    }
}
