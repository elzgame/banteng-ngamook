using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Transform x;
    public AudioClip sound;
    public GameObject healthPrefab;
    public GameObject healthParent;
    public int health;

    void Start()
    {
        x = FindObjectOfType<Player>().transform;
        for (int i = 0; i < health; i++)
        {
            var healthObj = Instantiate(healthPrefab, healthParent.transform.position, Quaternion.identity);
            healthObj.transform.SetParent(healthParent.transform);
            healthObj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }

    void Update()
    {
        transform.position = new Vector3(-(x.position.x), transform.position.y, x.position.z);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            int impact = 1;
            Player.health -= impact;
            for (int i = 0; i < impact; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Lives")[i].gameObject);
            }

            float bounce = 5f;
            Player.rb.AddForce(new Vector2(-3f, bounce), ForceMode2D.Impulse);

            GameManager.source.PlayOneShot(sound);
        }
    }



}
