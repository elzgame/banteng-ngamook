using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Transform x;

    void Start()
    {
        x = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        transform.position = new Vector3(-(x.position.x), transform.position.y, x.position.z);
    }
}
