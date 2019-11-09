using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Entity : MonoBehaviour//实体类
{
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    public double Mass
    {
        get => _rigidbody2D.mass;
    }
    public Vector2 Speed
    {
        get => _rigidbody2D.velocity;
    }

    protected int life;
    public int Life { get=>life; }

    protected EntityState state;
    public EntityState State => state;

    public abstract void Hurt(int damage);

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

public abstract class Bird : Entity
{
    public abstract void Skill();
    public override void Hurt(int damage)
    {
        //待处理
    }
}

public struct EntityState
{
    public int Full;//满状态是多少？（这由图片数量决定）
    public int Now;//当前状态

}
