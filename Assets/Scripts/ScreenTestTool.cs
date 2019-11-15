using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTestTool : MonoBehaviour
{
    public GameObject rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rb.GetComponent<Transform>().position = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition)+new Vector3(0,0,10);
            
        }
    }
}
