using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Entity : MonoBehaviour//实体类
{
    protected Rigidbody2D _rigidbody2D;
    protected Collider2D _collider2D;
    protected Transform _transform;
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
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        /*float x1 = _transform.position.x;
        float y1 = _transform.position.y;
        float x2 = other.transform.position.x;
        float y2 = other.transform.position.y;
        double theta = Math.Atan( (y1 - y2) / (x1 - x2) );
        double v1 = y1 * Math.Sin(theta) + x1 * Math.Cos(theta);
        double v2 = y1 * Math.Sin(theta) + x2 * Math.Cos(theta);
        double m1 = Mass;
        double m2 = other.rigidbody.mass;
        double v1E = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
        double i = m1 * (v1 - v1E);*/
        
        Hurt((int) (other.relativeVelocity.magnitude*other.rigidbody.mass*7.4) );
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
        if(life.Now<=0) Disappear();
    }
}

public struct Tube//试管型，储存满状态和当前状态
{
    public int Full;//满状态是多少？（这由图片数量决定）
    public int Now;//当前状态

}
