﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract  class Entity : MonoBehaviour//实体类
{
    public Rigidbody2D _rigidbody2D;
    protected Collider2D _collider2D;
    protected Transform _transform;
    protected Animator _animator;
    protected Animator _containerAnimator;

    public double Mass
    {
        get => _rigidbody2D.mass;
    }
    public Vector2 Speed
    {
        get => _rigidbody2D.velocity;
    }
    protected Tube life;
    public Tube Life { get=>life; }

    protected Tube state;
    public Tube State => state;
    protected abstract void Disappear();

    protected void InitializeReferences()//初始化对象引用
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _containerAnimator = GetComponentInParent<Animator>();
    }
    

    /*private void OnMouseDown()//测试
    {
        Debug.Log("my life is "+life.Now);
    }*/
}





public abstract class Bird : Entity
{
    protected Transform _anchorTransform;
    private bool fired = false;
    protected new void InitializeReferences()//初始化后取消鸟的模拟
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _containerAnimator = GetComponentsInParent<Animator>()[1];
        _anchorTransform = GetComponentsInParent<Transform>()[2];
        _rigidbody2D.simulated = false;
    }

    public void Jump()
    {
        _containerAnimator.SetTrigger("Jump");
    }

    public void JumpAndRoll()
    {
        _containerAnimator.SetTrigger("JumpAndRoll");
    }

    private IEnumerator Move33TimesIn330ms(Vector3 delta)
    {
        for (int i = 0; i < 33; i++)
        {
            _anchorTransform.Translate(delta);
            //transform.position = Vector3.MoveTowards();
            //Time.deltaTime
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator Start()
    {
        InitializeReferences();
        yield return new WaitForSeconds(Random.Range(0.0f,2.0f));
        _animator.SetTrigger("delayEnds");
    }

    public void JumpTo(Vector3 endPosition)//把上面那个函数挂进协程
    {
        
        _containerAnimator.SetTrigger("JumpHighAndRoll");
        Vector3 beginPosition = _transform.position;
        Vector3 disPer10ms = (endPosition-beginPosition)/33;
        disPer10ms.z = 0;
        StartCoroutine(Move33TimesIn330ms(disPer10ms));
    }

    public abstract void Skill();

    public void Ani_OnSling()
    {
        _animator.SetTrigger("onSling");
    }
    public void Ani_Fly()
    {
        fired = true;
        _animator.SetBool("flying",true);
    }
    public void Ani_HitGround()
    {
        _animator.SetBool("flying",false);
    }
    public void Ani_Disappear()
    {
        _animator.SetTrigger("disappear");
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        _animator.SetBool("flying", false);
    }

    private float _highSpeedLastTime;
    protected void Update()
    {
        if (fired)
        {
            if(_rigidbody2D.velocity.magnitude>0.07)_highSpeedLastTime = Time.time;
            if(Time.time-_highSpeedLastTime>5) Ani_Disappear();
            //Debug.Log("time now"+Time.time+"low begin"+_highSpeedLastTime);
        }
    }
}
public abstract class Block : Entity
{
    protected AllBirdsFloat BirdSensitivity;
    protected void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D point  = other.GetContact(0);
        Hurt((int) (point.normalImpulse*10) );
    }
    public void Hurt(int damage)
    {
        if (damage <= 10) return;
        life.Now -= damage;
        state.Now = life.Now / (life.Full / state.Full) + 1;
        if (state.Now < 1) state.Now = 1;
        //Debug.Log(_animator+";"+gameObject+"life now is "+life.Now+"/"+life.Full+",   state now is " + state.Now+"/"+state.Full);
        _animator.SetInteger("state",state.Now);
        if(life.Now<=0) Disappear();
    }

    public Block()
    {
        BirdSensitivity.Black = 1;
        BirdSensitivity.Blue = 1;
        BirdSensitivity.Green = 1;
        BirdSensitivity.Red = 1;
        BirdSensitivity.White = 1;
        BirdSensitivity.Yellow = 1;
    }

}

public struct Tube//试管型，储存满状态和当前状态
{
    public int Full;//满状态是多少？（这由图片数量决定）
    public int Now;//当前状态
}

public struct AllBirdsFloat
{
    public float Yellow;
    public float Red;
    public float Black;
    public float Blue;
    public float White;
    public float Green;
}


