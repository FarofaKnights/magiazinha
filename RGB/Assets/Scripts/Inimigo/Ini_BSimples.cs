using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini_BSimples : AtaqueBehaviour {
    GameObject target;
    Inimigo inimigo;

    Vector3 direcao = Vector3.zero;

    public float ataqueTime, attackSpeed;
    float currentAtaqueTimer = 0;

    public GameObject damageArea;

    void Start() {
        inimigo = GetComponent<Inimigo>();
        inimigo.ataque = this;
    }

    public override bool Atacando(GameObject target) {
        this.target = target;

        if (direcao == Vector3.zero) {
            Vector3 posDestino = target.transform.position;
            direcao = (posDestino - transform.position).normalized;
            inimigo.velocidade = direcao * attackSpeed;
            damageArea.SetActive(true);
        }

        if (currentAtaqueTimer < ataqueTime) {
            currentAtaqueTimer += Time.deltaTime;
        } else {
            currentAtaqueTimer = 0;
            direcao = Vector3.zero;
            damageArea.SetActive(false);
            return false;
        }

        return true;
    }
}
