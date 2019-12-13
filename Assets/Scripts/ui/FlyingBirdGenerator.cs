using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBirdGenerator : MonoBehaviour
{
    [SerializeField]private Sprite[] birdPics = null;
    [SerializeField]private GameObject flyingBird = null;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            if (Random.Range(0, 10) == 0)
            {
                GameObject bird = Instantiate(flyingBird, transform.position, Quaternion.identity);
                bird.GetComponent<SpriteRenderer>().sprite = birdPics[Random.Range(0, 4)];
                bird.GetComponent<Rigidbody2D>().velocity =
                    Vector2.up * Random.Range(3f, 10f) + Vector2.right * Random.Range(3f, 10f);
            }
            yield return new WaitForSeconds(0.5f);
        }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
