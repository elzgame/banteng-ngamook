using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    public float jumpHeight = 5f;
    public static int healthPoint = 100;
    private int jumpCount = 0;
    private bool buttonKanan;
    private bool buttonKiri;
    public AudioClip soundJump;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(moveX * moveSpeed, 0f);

        movement *= Time.deltaTime;

        transform.Translate(movement);


        if (buttonKanan == true)
        {
            float move = 1;

            Vector2 movementKanan = new Vector2(move * moveSpeed, 0f);

            movementKanan *= Time.deltaTime;

            transform.Translate(movementKanan);
        }

        if (buttonKanan == false)
        {
            Vector2 movementKanan = new Vector2(0f, 0f);

            movementKanan *= Time.deltaTime;

            transform.Translate(movementKanan);
        }

        if (buttonKiri == true)
        {
            float move = -1;

            Vector2 movementKiri = new Vector2(move * moveSpeed, 0f);

            movementKiri *= Time.deltaTime;

            transform.Translate(movementKiri);
        }

        if (buttonKiri == false)
        {
            Vector2 movementKiri = new Vector2(0f, 0f);

            movementKiri *= Time.deltaTime;

            transform.Translate(movementKiri);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }

    }

    public void KananDown()
    {
        buttonKanan = true;
    }

    public void KananUp()
    {
        buttonKanan = false;
    }


    public void KiriDown()
    {
        buttonKiri = true;
    }

    public void KiriUp()
    {
        buttonKiri = false;
    }

    public void Lompat()
    {
        jumpCount++;
        if (jumpCount <= 1)
        {
            GameManager.source.PlayOneShot(soundJump);
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }

        if (other.gameObject.tag == "Bomb")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);
            healthPoint -= impact;
            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Koruptor")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);
            healthPoint -= impact;
            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "OrangBaik")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);
            healthPoint += impact;
            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Lives")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);
            healthPoint += impact;
            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

    }



}
