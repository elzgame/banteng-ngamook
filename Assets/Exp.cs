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
        currentLevel = 1;
    }

    void Update()
    {
        if (Player.userExp >= maxExpLevel[currentLevel - 1])
        {
            Debug.Log("Naik level!");
            currentLevel++;
            Player.userExp = 0f;
            Debug.Log("Current Level : " + currentLevel.ToString());
            Debug.Log("Current Level : " + maxExpLevel[currentLevel - 1].ToString());
            slider.value = 0f;
        }

        currentLevelText.text = "LEVEL " + currentLevel.ToString();
        maxExpLevelText.text = "EXP : " + Player.userExp.ToString() + " / " + maxExpLevel[currentLevel - 1].ToString();
        gameObject.transform.localScale = new Vector3(slider.value, 1f, 1f);
        slider.value = Player.userExp / maxExpLevel[currentLevel - 1];
    }

}
