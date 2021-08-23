using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float startPost;
    public float parallaxSpeed;
    public float resetBG;
    public Vector2 myTransform;

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
        if(myTransform.x <= resetBG) {
            Debug.Log("RESET");
            transform.position =  new Vector2(startPost, transform.position.y);
        } 
    }
}
