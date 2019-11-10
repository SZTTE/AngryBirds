using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsManager : MonoBehaviour
{
    public static SpecialEffectsManager Instance;
    public GameObject smokeOrigin;
    public void Smoke(Vector2 position)
    {
        Instantiate(smokeOrigin, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }

    SpecialEffectsManager()
    {
        if(Instance==null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
}
