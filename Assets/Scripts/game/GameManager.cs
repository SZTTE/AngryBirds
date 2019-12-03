using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool _mute;


    public void MuteSwitch()
    {
        _mute = !_mute;
        Debug.Log("mute is "+_mute);
    }

    void Start()
    {
        _mute = false;
        if (Instance == null)   Instance = this;
        else                    Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
