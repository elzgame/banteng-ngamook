using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject orangBaik;
    public GameObject bomb;
    public GameObject lives;
    public GameObject koruptor;
    public GameObject boss;
    public GameObject weapon;
    public GameObject manuk;
    public Transform spawner;
    public Transform spawnerBoss;
    public static AudioSource source;
    public GameObject gameOverPanel;
    public AudioClip soundMoo;
    public AudioClip soundSpawn;
    private static int distance;
    public TextMeshProUGUI distanceText;
    public Text expText;
    public static float gameSpeed = 1f;
    public int newSpeed;
    private bool isBossAlready;

    void Start()
    {
        Time.timeScale = 1;
        source = GetComponent<AudioSource>();
        StartCoroutine(SpawningBomb());
        StartCoroutine(SpawningWeapon());
        StartCoroutine(SpawningLives());
        StartCoroutine(SpawningKoruptor());
        StartCoroutine(SpawningBoss());
        StartCoroutine(SpawningOrangBaik());
        StartCoroutine(SpawningManuk());
        StartCoroutine(DelayStartSound());
        StartCoroutine(CountScore());
        newSpeed = 10;
        distance = 0;
    }

    IEnumerator CountScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            distance++;
        }
    }

    IEnumerator DelayStartSound()
    {
        yield return new WaitForSeconds(0.5f);
        source.PlayOneShot(soundMoo);
        yield return new WaitForSeconds(0.3f);
        source.PlayOneShot(soundSpawn);
    }

    void Update()
    {

        if (distance >= newSpeed)
        {
            gameSpeed += 0.1f;
            newSpeed += 10;
            Debug.Log("TAMBAH KECEPATAN : " + gameSpeed);
        }

        distanceText.text = "JARAK : " + distance.ToString() + "M";

        if (Player.health <= 0)
        {
            StartCoroutine(GameOver());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(GameOver());
        }

    }

    IEnumerator GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        yield return 0;
    }


    public void RestartGame()
    {
        Time.timeScale = 1;
        Player.health = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawningBomb()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10 / gameSpeed, 12 / gameSpeed));
            Instantiate(bomb, spawner.transform.position, Quaternion.identity);
        }
    }

    IEnumerator SpawningManuk()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(6 / gameSpeed, 8 / gameSpeed));
            Instantiate(manuk, new Vector2(spawner.transform.position.x, 3.5f), Quaternion.identity);
        }
    }

    IEnumerator SpawningWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10 / gameSpeed, 25 / gameSpeed));
            Instantiate(weapon, spawner.transform.position, Quaternion.identity);
        }
    }



    IEnumerator SpawningLives()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15 / gameSpeed, 30 / gameSpeed));
            Instantiate(lives, spawner.transform.position, Quaternion.identity);
        }
    }

    IEnumerator SpawningKoruptor()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10 / gameSpeed, 15 / gameSpeed));
            Instantiate(koruptor, spawner.transform.position, Quaternion.identity);
        }
    }

    IEnumerator SpawningBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            if (isBossAlready == false)
            {
                Instantiate(boss, spawnerBoss.transform.position, Quaternion.identity);
                isBossAlready = true;
            }
        }
    }

    IEnumerator SpawningOrangBaik()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4 / gameSpeed, 7 / gameSpeed));
            Instantiate(orangBaik, spawner.transform.position, Quaternion.identity);
        }
    }




}
