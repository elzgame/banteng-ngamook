using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peluru : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.AddForce(new Vector2(speed, 0f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Ground" && other.gameObject.tag != "Destroy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Bomb" || other.gameObject.tag == "Manuk")
        {
            Debug.Log("Nambah exp 10!");
            Player.userExpPrefs += 10f;
        }

        if (other.gameObject.tag == "Koruptor")
        {
            Debug.Log("Nambah exp 10!");
            Player.userExpPrefs += 30f;
        }

        if (other.gameObject.tag == "OrangBaik")
        {
            Debug.Log("Duar nyawamu berkurang 1!");
            Player.health--;

            for (int i = 0; i < 1; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Lives")[i].gameObject);
            }

        }


        if (other.gameObject.tag == "Destroy")
        {
            StartCoroutine(MissNotification());
            Debug.Log(GameObject.Find("Misses").GetComponentInChildren<RectTransform>(true).gameObject.name);
            Debug.Log("Miss!");
        }
    }

    IEnumerator MissNotification()
    {
        GameObject.Find("Misses").GetComponentInChildren<RectTransform>(true).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(.3f);
        GameObject.Find("Misses").GetComponentInChildren<RectTransform>(true).gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

}
