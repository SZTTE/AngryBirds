using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBirdScript : Bird
{
    public RedBirdScript()
    {
        life.Full = 1000;
        life.Now = 1000;
        state.Full = 2;//鸟最健康的时候在第二状态
        state.Now = 2;
    }

    public override void Skill()
    {
        //待完成
    }

    public void Smoke()//由动画启动
    {
        SpecialEffectsManager.Instance.Smoke(_transform.position); //这边为什么可以赋给一个vec2的值？
    }

    protected override void Disappear()
    {
        
    }

    private void DestroyMe() //由动画启动
    {
        SpecialEffectsManager.Instance.Feathers(transform.position,feather1,feather2,feather3);
        Destroy(gameObject);
    }
}
