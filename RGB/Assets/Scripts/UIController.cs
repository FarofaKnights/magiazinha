using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public GameObject shopPanel, detailPanel;

    public static UIController instance;

    public Text powerText;

    public GameObject[] vidas;

    void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start() {
        shopPanel.SetActive(false);
    }

    public void UpdatePowerText() {
        powerText.text = GameManager.instance.power.ToString();
    }

    public void UpdateLife(float amount) {
        float resto = amount % 1;
        int vidasInt = (int) amount;

        for (int i = 0; i < vidas.Length; i++) {
            if (i < vidasInt) {
                vidas[i].SetActive(true);
            } else {
                vidas[i].SetActive(false);
            }
        }


    }
}
