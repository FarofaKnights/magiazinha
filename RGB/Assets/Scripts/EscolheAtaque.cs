using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscolheAtaque : MonoBehaviour {
    public string arma = "Soco";

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<Player>().SelectArma(arma);
            Destroy(gameObject);
        }
    }
}
