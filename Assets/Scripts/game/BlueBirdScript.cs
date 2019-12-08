using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirdScript : Bird
{
    [SerializeField] private GameObject clone;
    public BlueBirdScript()
    {
        life.Full = 1000;
        life.Now = 1000;
        state.Full = 2;//鸟最健康的时候在第二状态
        state.Now = 2;
    }

    public void BeAClone()
    {
        Debug.Log(_animator);
        /*_animator.SetTrigger("clone");
        _trailRenderer.enabled = true;*/
    }

    public override void Skill()
    {
        GameObject b1 = Instantiate(clone, transform.position+0.5f*Vector3.up, Quaternion.identity);
        b1.GetComponent<Rigidbody2D>().velocity= _rigidbody2D.velocity+1f*Vector2.up;
        GameObject b2 = Instantiate(clone, transform.position+0.5f*Vector3.down, Quaternion.identity);
        b2.GetComponent<Rigidbody2D>().velocity=_rigidbody2D.velocity-1f*Vector2.up;
    }

    public void Smoke()//由动画启动
    {
        SpecialEffectsManager.Instance.Smoke(_transform.position); //这边为什么可以赋给一个vec2的值？
    }

    protected override void Disappear()
    {
        
    }
}
