using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueInimigo : MonoBehaviour {
    public Tipo tipo = Tipo.Neutro;
    public int dano = 1;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Player player = other.GetComponent<Player>();

            if (player != null) {
                player.SofrerDano(dano, tipo);
            }
        }
    }
}
