using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Rigidbody2D rb;
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
    public GameObject currentItem;
    public Transform itemPosition;
    private bool newItemPicked;
    public TextMeshProUGUI ammoText;
    private int ammo = 0;
    public GameObject shootButton;
    public AudioClip soundShoot;
    public AudioClip soundUgh;
    public GameObject ammoPrefab;
    public static float userExpPrefs;
    public static int levelPrefs;

    void Start()
    {

        userExpPrefs = PlayerPrefs.GetFloat("userExpPrefs", 0);
        levelPrefs = PlayerPrefs.GetInt("levelPrefs", 1);


        rb = GetComponent<Rigidbody2D>();
        for (int i = 0; i < health; i++)
        {
            var healthObj = Instantiate(healthPrefab, healthParent.transform.position, Quaternion.identity);
            healthObj.transform.SetParent(healthParent.transform);
            healthObj.transform.localScale = new Vector3(1f,1f,1f);
        }
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(moveX * moveSpeed, 0f);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        rotationZ = transform.rotation.z;

        if (newItemPicked == true)
        {
            newItemPicked = false;
            var createWeapon = Instantiate(currentItem, itemPosition.transform.position, Quaternion.Euler(0f, 180f, 0));
            currentItem.SetActive(true);
            createWeapon.transform.SetParent(itemPosition.transform);
            Debug.Log("Item changed!");
            currentItem = createWeapon.gameObject;
        }

        if (currentItem != null)
        {
            ammoText.gameObject.SetActive(true);
            ammoText.text = "AMMO : " + ammo.ToString();
            shootButton.SetActive(true);
        }

        ammoText.text = "AMMO : " + ammo.ToString();

        if (ammo <= 0 && currentItem != null)
        {
            Destroy(currentItem.gameObject);
            currentItem = null;
            shootButton.SetActive(false);
            ammoText.gameObject.SetActive(false);
            var banyakSenjata = GameObject.FindGameObjectsWithTag("Weapon").Length;
            for (int x = 0; x < banyakSenjata; x++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Weapon")[x].gameObject);
            }
        }

        // if (changeHealth == true)
        // {
        //     Debug.Log(health);
        //     for (int j = 0; j < healthParent.transform.childCount; j++)
        //     {
        //         Destroy(GameObject.FindGameObjectsWithTag("Lives")[j]);
        //     }
        //     // StartCoroutine(HealthDelay());
        //     changeHealth = false;
        // }

        // IEnumerator HealthDelay() {
        //     yield return new WaitForSeconds(.001f);
        //     for (int i = 0; i < health; i++)
        //     {
        //         var healthObj = Instantiate(healthPrefab, healthParent.transform.position, Quaternion.identity);
        //         healthObj.transform.SetParent(healthParent.transform);
        //     }
        // }

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


    public void Shoot()
    {
        Debug.Log("TEMBAKKK!");
        ammo--;
        Instantiate(ammoPrefab, currentItem.GetComponentInChildren<Transform>().gameObject.transform.position, currentItem.GetComponentInChildren<Transform>().gameObject.transform.rotation);
        GameManager.source.PlayOneShot(soundUgh);
        GameManager.source.PlayOneShot(soundShoot);
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

            for (int i = 0; i < impact; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Lives")[i].gameObject);
            }

            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }


        if (other.gameObject.tag == "Manuk")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);
            health -= impact;

            for (int i = 0; i < impact; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Lives")[i].gameObject);
            }

            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Koruptor")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);

            Debug.Log("Nambah exp 15!");
            Player.userExpPrefs += 15f;

            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "OrangBaik")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            rb.AddForce(Vector2.up * 5f);
            health -= impact;

            for (int i = 0; i < impact; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Lives")[i].gameObject);
            }


            GameManager.source.PlayOneShot(sound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Lives")
        {
            int impact = other.gameObject.GetComponent<MoveToLeft>().impactScore;
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            GameManager.source.PlayOneShot(sound);
            rb.AddForce(Vector2.up * 5f);
            health += impact;

            for (int i = 0; i < impact; i++)
            {
                var children = Instantiate(healthPrefab, healthParent.transform);
                children.transform.SetParent(healthParent.transform);
                children.transform.localScale = new Vector3(1f,1f,1f);
            }

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Weapon")
        {
            AudioClip sound = other.gameObject.GetComponent<MoveToLeft>().sound;
            GameManager.source.PlayOneShot(sound);
            currentItem = other.gameObject;
            currentItem.transform.localScale = new Vector3(currentItem.transform.localScale.x / 3, currentItem.transform.localScale.y / 3, currentItem.transform.localScale.z);
            Debug.Log("Weapon picked!");
            newItemPicked = true;
            Destroy(other.gameObject.GetComponent<BoxCollider2D>());
            Destroy(other.gameObject.GetComponent<Rigidbody2D>());
            Destroy(other.gameObject.GetComponent<MoveToLeft>());
            ammo += other.gameObject.GetComponent<MoveToLeft>().impactScore;
            StartCoroutine(DestroyNeedTime(other.gameObject));
        }

    }

    IEnumerator DestroyNeedTime(GameObject x)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(x.gameObject);
    }



}
