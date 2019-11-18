using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlingScript : MonoBehaviour
{
    private Transform _transform;
    public List<Bird> birds;
    private Tube _birdNumber;//鸟的计数器，鸟的编号从零开始
    //重新装填一只鸟
    public void Reload()
    {
        birds[_birdNumber.Now].JumpTo(_transform.position);
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _birdNumber.Full = birds.Count-1;
        _birdNumber.Now = 0;
        _transform = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Reload();
    }
}
