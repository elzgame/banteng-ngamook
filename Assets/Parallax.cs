using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float startPost;
    public float parallaxSpeed;
    public float resetBG;
    public Vector2 myTransform;
    public Vector2 myTransformSprite;
    public GameObject BGSprite;

    // Start is called before the first frame update
    void Start()
    {
        startPost = transform.position.x;
        resetBG = -28.15f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * parallaxSpeed / 10, Space.Self);
        myTransform = transform.position;
        if (myTransform.x <= resetBG)
        {
            Debug.Log("RESET");
            transform.position = new Vector2(startPost, transform.position.y);
        }

        BGSprite.transform.Translate(Vector2.left * Time.deltaTime * parallaxSpeed / 10, Space.Self);
        myTransformSprite = BGSprite.transform.position;
        if (myTransformSprite.x <= resetBG)
        {
            BGSprite.transform.position = new Vector2(startPost, BGSprite.transform.position.y);
        }
    }
}
