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
        //测试功能
        if (Input.GetKeyDown(KeyCode.A))
        {
            //rb.GetComponent<Transform>().position = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition)+new Vector3(0,0,10);
            //rb.GetComponent<RedBirdScript>().JumpTo(new Vector3(3,3,3));
            //SettlementCanvasScript.Instance.Appear(3500);
            //SpecialEffectsManager.Instance.Score(new Vector2(0,0),null );
        }
    }
}
