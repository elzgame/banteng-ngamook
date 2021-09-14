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
    public int bossSpawnDistance;

    void Start()
    {
        Time.timeScale = 1;
        source = GetComponent<AudioSource>();
        StartCoroutine(SpawningBomb());
        StartCoroutine(SpawningWeapon());
        StartCoroutine(SpawningLives());
        StartCoroutine(SpawningKoruptor());
        StartCoroutine(SpawningOrangBaik());
        StartCoroutine(SpawningManuk());
        StartCoroutine(DelayStartSound());
        StartCoroutine(CountScore());
        StartCoroutine(SpawningBoss());
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
            if (isBossAlready == false)
            {
                Instantiate(bomb, spawner.transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator SpawningManuk()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(6 / gameSpeed, 8 / gameSpeed));
            if (isBossAlready == false)
            {
                Instantiate(manuk, new Vector2(spawner.transform.position.x, 3.5f), Quaternion.identity);
            }
        }
    }

    IEnumerator SpawningWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10 / gameSpeed, 25 / gameSpeed));
            if (isBossAlready == false)
            {
                Instantiate(weapon, spawner.transform.position, Quaternion.identity);
            }
        }
    }



    IEnumerator SpawningLives()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15 / gameSpeed, 30 / gameSpeed));
            if (isBossAlready == false)
            {
                Instantiate(lives, new Vector2(spawner.transform.position.x, 1.27f), Quaternion.identity);
            }
        }
    }

    IEnumerator SpawningKoruptor()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10 / gameSpeed, 15 / gameSpeed));
            if (isBossAlready == false)
            {
                Instantiate(koruptor, spawner.transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator SpawningBoss()
    {
        while (true)
        {
            int multiplier = 1;
            int seconds = bossSpawnDistance * multiplier;
            yield return new WaitForSeconds(seconds);
            if (isBossAlready == false)
            {
                Instantiate(boss, spawnerBoss.transform.position, Quaternion.identity);
                multiplier *= 2;
                isBossAlready = true;
                Debug.Log("Spawning Boss!");
            }

        }
    }

    IEnumerator SpawningOrangBaik()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4 / gameSpeed, 7 / gameSpeed));
            if (isBossAlready == false)
            {
                Instantiate(orangBaik, spawner.transform.position, Quaternion.identity);
            }
        }
    }




}
