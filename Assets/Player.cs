using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    public float jumpHeight = 5f;
    public static int health = 3;
    public GameObject healthParent;
    public GameObject healthPrefab;
    private int jumpCount = 0;
    private bool buttonKanan;
    private bool buttonKiri;
    public AudioClip soundJump;
    public float rotationZ = 0;
    private bool changeHealth;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        for (int i = 0; i < health; i++)
        {
            var healthObj = Instantiate(healthPrefab, healthParent.transform.position, Quaternion.identity);
            healthObj.transform.SetParent(healthParent.transform);
        }
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(moveX * moveSpeed, 0f);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        rotationZ = transform.rotation.z;


        if (changeHealth == true)
        {
            for (int j = 0; j < healthParent.transform.childCount; j++)
            {
                Debug.Log(j);
                Destroy(GameObject.FindGameObjectsWithTag("Lives")[j]);
            }

            for (int i = 0; i < health; i++)
            {
                Debug.Log(i);
                var healthObj = Instantiate(healthPrefab, healthParent.transform.position, Quaternion.identity);
                healthObj.transform.SetParent(healthParent.transform);
            }
            changeHealth = false;
        }

        if (rotationZ <= -0.7f)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 0f * Time.deltaTime);
        }

        if (rotationZ >= 0.7f)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 0f * Time.deltaTime);
        }

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
            health -= impact;
            changeHealth = true;
            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Koruptor")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);
            health -= impact;
            changeHealth = true;
            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "OrangBaik")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);
            health += impact;
            changeHealth = true;
            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Lives")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);
            health += impact;
            changeHealth = true;
            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

    }



}
