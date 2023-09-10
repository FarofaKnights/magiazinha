using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour {
    public enum Estado { Atacando, Seguindo, Parado }
    public Estado estado = Estado.Parado;

    public float paradoTime, raioProximidade, speed, waitAttackTime;
    public Vector3 velocidade;
    float lastParadoTime, currentAttackTime;

    GameObject alvo;
    Atacavel atacavel;

    public GameObject redEffect, blueEffect, greenEffect;

    public GameObject dropPrefab;
    public int quantidadeDrop = 1;

    public AtaqueBehaviour ataque;


    NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        atacavel = GetComponent<Atacavel>();
        alvo = GameManager.instance.player.gameObject;
        
        if (atacavel.tipo == Tipo.Neutro) {
            atacavel.GerarTipoAleatorio();
            if (atacavel.tipo == Tipo.Vermelho) redEffect.SetActive(true);
            else if (atacavel.tipo == Tipo.Azul) blueEffect.SetActive(true);
            else if (atacavel.tipo == Tipo.Verde) greenEffect.SetActive(true);
        }
    }

    void FixedUpdate() {
        if (estado == Estado.Parado) Parado();
        else if (estado == Estado.Seguindo) Seguir();
        else if (estado == Estado.Atacando) {
            if (ataque == null || !ataque.Atacando(alvo)) Parar();
        }
        
        agent.velocity = velocidade;
        transform.LookAt(alvo.transform.position);
    }

    void OnDestroy() {
        if (atacavel.isDying) {
            Drop();
        }
    }

    public void Drop() {
        for (int i = 0; i < quantidadeDrop; i++) {
            GameObject drop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

            float angulo = Random.Range(0, 360);
            Vector3 direcao = Quaternion.AngleAxis(angulo, Vector3.up) * transform.forward;
            drop.GetComponent<Rigidbody>().AddForce(direcao.normalized * 5, ForceMode.Impulse);
        }
    }

    public void Parar() {
        lastParadoTime = 0;
        estado = Estado.Parado;
        velocidade = Vector3.zero;
    }

    public virtual void Parado() {
        lastParadoTime += Time.fixedDeltaTime;

        if (lastParadoTime >= paradoTime) {
            lastParadoTime = 0;
            estado = Estado.Seguindo;
        }
    }

    public virtual void Seguir() {
        if (alvo == null) {
            estado = Estado.Parado;
            return;
        }

        Vector3 posDestino = alvo.transform.position;
        Vector3 direcao = (posDestino - transform.position).normalized;

        float dist = Vector3.Distance(transform.position, posDestino);

        // O destino é estar a uma certa distância do alvo, não exatamente no alvo
        if (dist < raioProximidade) {
            direcao = (transform.position - posDestino).normalized;
            posDestino += direcao * (raioProximidade - dist);
        } else {
            posDestino -= direcao * raioProximidade;
        }


        if (Mathf.Abs(raioProximidade-dist) > 0.1f) {
            velocidade = direcao * speed;
        } else {
            velocidade = Vector3.zero;
            currentAttackTime += Time.deltaTime;

            if (currentAttackTime >= waitAttackTime) {
                currentAttackTime = 0;
                estado = Estado.Atacando;
            }
        }

    }
}
