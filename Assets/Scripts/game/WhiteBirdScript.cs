using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBirdScript : Bird
{
    [SerializeField] private GameObject egg;
    public WhiteBirdScript()
    {
        life.Full = 1000;
        life.Now = 1000;
        state.Full = 2;//鸟最健康的时候在第二状态
        state.Now = 2;
    }
    public override void JumpTo(Vector3 endPosition)//把上面那个函数挂进协程
    {
        _transform.parent = null;
        Vector3 beginPosition = _transform.position;
        endPosition+=Vector3.up*0.2f;
        endPosition+=Vector3.right*0.1f;
        Vector3 disPer10ms = (endPosition-beginPosition)/33;
        disPer10ms.z = 0;
        StartCoroutine(Move33TimesIn330ms(disPer10ms));
    }

    public override void Skill()
    {
        _animator.SetTrigger("empty");
        _rigidbody2D.velocity = Vector2.up*20+Vector2.right*10;
        Instantiate(egg, transform.position + Vector3.down * 0.7f, Quaternion.identity);
        Invoke("DestroyMe",7);
    }

    public void Smoke()//由动画启动
    {
        SpecialEffectsManager.Instance.Smoke(_transform.position); //这边为什么可以赋给一个vec2的值？
    }

    protected override void Disappear()
    {
        
    }
}
