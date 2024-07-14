using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShowcase : MonoBehaviour {
    GameObject corpo;
    public Arma arma;

    string[] weapons = { "Espada", "Soco", "Cajado" };
    int currentWeapon = 0;

    void Start() {
        corpo = transform.Find("Corpo").gameObject;
        SelectArma("Cajado");
    }

    void Update() {
        if (arma != null) {
            // Ataque
            if (Input.GetMouseButtonDown(0)) {
                arma.Atacar();
            } else if (Input.GetMouseButtonDown(1)) {
                arma.AtacarEspecial();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Tab)) {
            currentWeapon = (currentWeapon + 1) % weapons.Length;
            SelectArma(weapons[currentWeapon]);
        }
    }

    public void SelectArma(string nome) {
        int pos = -1;

        for (int i = 0; i < weapons.Length; i++) {
            if (weapons[i] == nome) {
                pos = i;
                break;
            }
        }

        if (pos == -1) return;

        if (arma != null) {
            MonoBehaviour script = (MonoBehaviour)arma;
            script.gameObject.SetActive(false);
        }

        GameObject armaObj = corpo.transform.Find(nome).gameObject;
        armaObj.SetActive(true);
        arma = armaObj.GetComponent<Arma>();

        currentWeapon = pos;
    }

    public string GetArma() {
        if (arma == null) return "";

        MonoBehaviour script = (MonoBehaviour)arma;
        return script.gameObject.name;
    }
}
