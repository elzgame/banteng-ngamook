using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLeft : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    private Vector3 originalScale;
    public int impactScore;
    public AudioClip sound;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        MoveToDirection(Vector2.left);
    }


    private void MoveToDirection(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;

        transform.localScale = new Vector3(direction.x < 0 ? -originalScale.x : originalScale.x, originalScale.y,
            originalScale.z);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag  == "Destroy") {
            Destroy(this.gameObject);
        }

    }

}
