using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini_GSimples : AtaqueBehaviour {
    public enum Estado { Aproximando, Atacando }
    public Estado estado = Estado.Aproximando;

    public float aproximaSpeed, aproximaTimer, atacaSpeed, atacaTimer;
    float currentAtacaTimer, currentAproximaTimer = 0;

    GameObject target;
    Inimigo inimigo;

    void Start() {
        inimigo = GetComponent<Inimigo>();
        inimigo.ataque = this;
    }

    public override bool Atacando(GameObject target) {
        this.target = target;

        if (estado == Estado.Aproximando) Aproximando();
        else if (estado == Estado.Atacando) {
            if (currentAtacaTimer < atacaTimer) {
                currentAtacaTimer += Time.deltaTime;
            } else {
                currentAtacaTimer = 0;
                estado = Estado.Aproximando;
                return false;
            }
        }

        return true;
    }

    public void Aproximando() {
        Vector3 posDestino = target.transform.position;
        Vector3 direcao = (posDestino - transform.position).normalized;

        float dist = Vector3.Distance(transform.position, posDestino);

        if (currentAproximaTimer < aproximaTimer) {
            inimigo.velocidade = direcao * aproximaSpeed;
            currentAproximaTimer += Time.deltaTime;
        } else {
            estado = Estado.Atacando;
            currentAtacaTimer = 0;
            currentAproximaTimer = 0;
            inimigo.velocidade = direcao * atacaSpeed;
        }
    }
}
