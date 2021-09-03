using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exp : MonoBehaviour
{

    public Slider slider;
    public int[] maxExpLevel;
    public Text maxExpLevelText;
    public int currentLevel;
    public Text currentLevelText;


    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("levelPrefs", 1);
    }

    void Update()
    {
        if (Player.userExpPrefs >= maxExpLevel[currentLevel - 1])
        {
            Debug.Log("Naik level!");
            currentLevel++;
            Player.userExpPrefs = 0f;
            Debug.Log("Current Level : " + currentLevel.ToString());
            Debug.Log("Current Level : " + maxExpLevel[currentLevel - 1].ToString());
            slider.value = 0f;
        }

        PlayerPrefs.SetInt("levelPrefs", currentLevel);
        PlayerPrefs.SetFloat("userExpPrefs", Player.userExpPrefs);

        currentLevelText.text = "LEVEL " + currentLevel.ToString();
        maxExpLevelText.text = "EXP : " + Player.userExpPrefs.ToString() + " / " + maxExpLevel[currentLevel - 1].ToString();
        slider.value = Player.userExpPrefs / maxExpLevel[currentLevel - 1];
        gameObject.transform.localScale = new Vector3(slider.value, 1f, 1f);
    }

}
