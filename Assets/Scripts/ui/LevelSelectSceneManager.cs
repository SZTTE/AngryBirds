using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Image = UnityEngine.UI.Image;

public class LevelSelectSceneManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> levelButton = null;
    [SerializeField]private Sprite disabledButton = null;

    void Start()
    {
        Tube level = GameManager.Instance.Level;
        /*for (int i = 0; i < level.Now; i++)
        {
            levelButton[i].
        }*/
        for (int i = level.Now; i < level.Full; i++)
        {
            levelButton[i].GetComponent<Image>().sprite = disabledButton;
            levelButton[i].GetComponent<UnityEngine.UI.Button>().enabled = false;
        }
        
    }
}
