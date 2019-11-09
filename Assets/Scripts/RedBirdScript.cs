using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBirdScript : Bird
{
    public RedBirdScript()
    {
        life = 1000;
        state.Full = 2;//鸟最健康的时候在第二状态
        state.Now = 2;
    }

    public override void Skill()
    {
        //待完成
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
