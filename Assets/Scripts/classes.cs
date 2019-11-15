using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Entity : MonoBehaviour//实体类
{
    protected Rigidbody2D _rigidbody2D;
    protected Collider2D _collider2D;
    protected Transform _transform;
    protected Animator _animator;
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
    public abstract void Hurt(int damage);
    protected abstract void Disappear();

    protected void InitializeReferences()//初始化对象引用
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D point  = other.GetContact(0);
        Hurt((int) point.normalImpulse*50);
    }
}

public abstract class Bird : Entity
{
    public abstract void Skill();
    public override void Hurt(int damage)
    {
        life.Now -= damage;
        state.Now = life.Now / (life.Full / state.Full) + 1;
        Debug.Log("life now is "+life.Now+"/"+life.Full+",   state now is " + state.Now+"/"+state.Full);
        _animator.SetInteger("state",state.Now);
        if(life.Now<=0) Disappear();
    }
}
public abstract class Block : Entity
{
    protected AllBirdsFloat BirdSensitivity;
    public override void Hurt(int damage)
    {
        life.Now -= damage;
        state.Now = life.Now / (life.Full / state.Full) + 1;
        Debug.Log("life now is "+life.Now+"/"+life.Full+",   state now is " + state.Now+"/"+state.Full);
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


