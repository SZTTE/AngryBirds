using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirdScript : Bird
{
    [SerializeField] private GameObject clone = null;
    public static GameObject b1;
    public static GameObject b2;
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
        b1 = Instantiate(clone, transform.position+0.5f*Vector3.up, Quaternion.identity);
        b1.GetComponent<Rigidbody2D>().velocity= _rigidbody2D.velocity+1f*Vector2.up;
        b2 = Instantiate(clone, transform.position+0.5f*Vector3.down, Quaternion.identity);
        b2.GetComponent<Rigidbody2D>().velocity=_rigidbody2D.velocity-1f*Vector2.up;
    }

    public void Smoke()//由动画启动
    {
        SpecialEffectsManager.Instance.Smoke(_transform.position); //这边为什么可以赋给一个vec2的值？
    }
    public override void DeleteTrait()
    {
        TrailScript.Instance.Clear();
        Destroy(b2);
        Destroy(b1);
    }

    protected override void Disappear()
    {
        
    }
}
