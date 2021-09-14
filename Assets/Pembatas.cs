using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pembatas : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player touched!");
            Vector3 currentHigh = other.gameObject.transform.position;
            other.gameObject.transform.position  = new Vector3(-7.6402f, currentHigh.y, 0f);
        }
    }

}
