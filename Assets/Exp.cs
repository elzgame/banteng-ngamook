using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exp : MonoBehaviour
{

    public Slider slider;


    void Update() {
        slider.value = Player.userExp / 100f;
        gameObject.transform.localScale = new Vector3(slider.value, 1f, 1f);
    }

}
