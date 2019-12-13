using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animation>().Play("begin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevel1()
    {
        GameManager.Instance.GotoLevel(1);
    }
}
