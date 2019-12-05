using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasingPic : MonoBehaviour
{
    [SerializeField]private float width = 0;
    [SerializeField]private float speed = 0;
    //[SerializeField] private int sum = 0;
    private Rigidbody2D _rigidbody;

    private bool haveCopied = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = Vector2.left*speed;
    }

    
    void Update()
    {
        if (transform.position.x < 10 && !haveCopied)
        {
            Instantiate(gameObject, transform.position + Vector3.right * (width), Quaternion.identity);
            haveCopied = true;
        }
        //if(transform.position.x<-20) Destroy(gameObject);
    }

    
}
