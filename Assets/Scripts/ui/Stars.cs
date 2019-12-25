using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    [SerializeField]private Sprite starLight = null;
    [SerializeField]private Sprite starEmpty = null;
    [SerializeField]private Image star1 = null;
    [SerializeField]private Image star2 = null;
    [SerializeField]private Image star3 = null;
    [SerializeField]private int level = 0;
    public static Stars[] AllStars = new Stars[50];
    // Start is called before the first frame update
    void Start()
    {
        AllStars[level] = this;
        switch (GameManager.Instance.levelStars[level])
        {
            case 0:
                star1.sprite = starEmpty;
                star2.sprite = starEmpty;
                star3.sprite = starEmpty;
                break;
            case 1:
                star1.sprite = starLight;
                star2.sprite = starEmpty;
                star3.sprite = starEmpty;
                break;
            case 2:
                star1.sprite = starLight;
                star2.sprite = starLight;
                star3.sprite = starEmpty;
                break;
            case 3:
                star1.sprite = starLight;
                star2.sprite = starLight;
                star3.sprite = starLight;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
