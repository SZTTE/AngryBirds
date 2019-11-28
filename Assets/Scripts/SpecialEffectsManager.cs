using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsManager : MonoBehaviour
{
    public static SpecialEffectsManager Instance;
    public GameObject smokeOrigin;
    public GameObject featherGenerators;
    /// <summary>
    /// 生成烟雾
    /// </summary>
    /// <param name="position">产生烟雾的位置</param>
    public void Smoke(Vector2 position)
    {
        Instantiate(smokeOrigin, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }

    /// <summary>
    /// 在某处生成羽毛粒子
    /// </summary>
    public void Feathers(Vector2 position, Sprite f1, Sprite f2, Sprite f3)
    {
        GameObject _featherGenerators =
            Instantiate(featherGenerators, new Vector3(position.x, position.y, 0), Quaternion.identity);
        ParticleSystem[] particle = _featherGenerators.GetComponentsInChildren<ParticleSystem>();
        particle[0].textureSheetAnimation.SetSprite(0,f1);
        particle[1].textureSheetAnimation.SetSprite(0,f2);
        particle[2].textureSheetAnimation.SetSprite(0,f3);
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
