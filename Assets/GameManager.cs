using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject orangBaik;
    public GameObject bomb;
    public GameObject lives;
    public GameObject koruptor;
    public Transform spawner;
    public static AudioSource source;
    public GameObject gameOverPanel;
    public AudioClip soundMoo;
    public AudioClip soundSpawn;

    void Start()
    {
        Time.timeScale = 1;
        source = GetComponent<AudioSource>();
        StartCoroutine(SpawningBomb());
        StartCoroutine(SpawningLives());
        StartCoroutine(SpawningKoruptor());
        StartCoroutine(SpawningOrangBaik());
        StartCoroutine(DelayStartSound());
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
            yield return new WaitForSeconds(Random.Range(2, 4));
            Instantiate(bomb, spawner.transform.position, Quaternion.identity);
        }
    }

    IEnumerator SpawningLives()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 8));
            Instantiate(lives, spawner.transform.position, Quaternion.identity);
        }
    }

    IEnumerator SpawningKoruptor()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 10));
            Instantiate(koruptor, spawner.transform.position, Quaternion.identity);
        }
    }

    IEnumerator SpawningOrangBaik()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 9));
            Instantiate(orangBaik, spawner.transform.position, Quaternion.identity);
        }
    }




}
