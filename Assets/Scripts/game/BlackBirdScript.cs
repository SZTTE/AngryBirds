using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBirdScript : Bird
{
    private Collider2D _explosionCollider;
    private bool exploding = false;
    [SerializeField] private AudioClip explosionSound = null;

    public BlackBirdScript()
    {
        life.Full = 1000;
        life.Now = 1000;
        state.Full = 2;//鸟最健康的时候在第二状态
        state.Now = 2;
    }
    
    protected override void InitializeReferences()//初始化后取消鸟的模拟
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _containerAnimator = GetComponentsInParent<Animator>()[1];
        _anchorTransform = GetComponentsInParent<Transform>()[2];
        _trailRenderer = GetComponent<TrailRenderer>();
        _audioSource = GetComponentInChildren<AudioSource>();
        _explosionCollider = GetComponents<Collider2D>()[1];
        _rigidbody2D.simulated = false;
    }

    public override void Skill()
    {
        TrailScript.Instance.StopDrawing();
        _rigidbody2D.simulated = false;
        skillUsed = true;
        exploding = true;
        _animator.SetTrigger("explode");
        AudioManager.Instance.Play(explosionSound,1);
    }

    public void Smoke()//由动画启动
    {
        SpecialEffectsManager.Instance.Smoke(_transform.position); //这边为什么可以赋给一个vec2的值？/
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (exploding)
        {
            Debug.Log("exploding");
            Vector2 deltaPosition = other.transform.position - transform.position;
            Vector2 direction = deltaPosition / deltaPosition.magnitude;
            other.GetComponent<Rigidbody2D>().AddForce(200f * (1.5f - deltaPosition.magnitude) * direction);
        }
    }

    protected override void DestroyMe() //由动画启动
    {
        Destroy(gameObject);
    }
    protected override void Disappear()
    {
        
    }
    
    protected override void Update()
    {
        if (fired)
        {
            if(_rigidbody2D.velocity.magnitude>0.5f)_highSpeedLastTime = Time.time;
            if(Time.time-_highSpeedLastTime>5) Ani_Disappear();
            //Debug.Log("time now"+Time.time+"low begin"+_highSpeedLastTime);
        }
        ImStillAlive();
        if (!skillUsed && fired && Input.GetMouseButtonDown(0))
        {
            Skill();
            skillUsed = true;
        }
    }
}
