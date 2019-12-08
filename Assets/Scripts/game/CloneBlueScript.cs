using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBlueScript : MonoBehaviour
{
    [SerializeField] protected Sprite feather1;
    [SerializeField] protected Sprite feather2;
    [SerializeField] protected Sprite feather3;
    private TrailRenderer _trailRenderer;
    private Rigidbody2D _rigidbody2D;
    private float lastTimeHighSpeed;
    private SpriteRenderer _spriteRenderer;
    private bool disappeared = false;
    void Start()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _trailRenderer.emitting = false;
    }

    private void Update()
    {
        if (_rigidbody2D.velocity.magnitude < 0.3f && !disappeared)
        {
            Disappear();
            disappeared = true;
        }
        
    }

    private void Disappear()
    {
        SpecialEffectsManager.Instance.Feathers(transform.position,feather1,feather2,feather3);
        SpecialEffectsManager.Instance.Smoke(transform.position);
        _spriteRenderer.enabled = false;
    }
}
