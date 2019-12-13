using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private bool exploding = false;
    [SerializeField] private AudioClip explosionSound = null;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!exploding)   AudioManager.Instance.Play(explosionSound,1);
        exploding = true;
        _animator.SetTrigger("Explode");
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (exploding)
        {
            Vector2 deltaPosition = other.transform.position - transform.position;
            Vector2 direction = deltaPosition / deltaPosition.magnitude;
            other.GetComponent<Rigidbody2D>().AddForce(50f * (3f - deltaPosition.magnitude)* (3f - deltaPosition.magnitude) * direction);
        }
    }
}
