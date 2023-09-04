using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour {
    public enum Estado { Vagando, Atacando, Seguindo, Fugindo, Parado }
    public Estado estado = Estado.Vagando;

    public float speed, maxWanderTime, maxParadoTime, raioProcura;
    Vector3 velocidade;
    float lastWanderTime, lastParadoTime, wanderTime, paradoTime;

    GameObject alvo;

    Atacavel atacavel;

    public GameObject redEffect, blueEffect, greenEffect;


    NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        atacavel = GetComponent<Atacavel>();
        
        if (atacavel.tipo == Tipo.Neutro) atacavel.GerarTipoAleatorio();

        if (atacavel.tipo == Tipo.Vermelho) redEffect.SetActive(true);
        else if (atacavel.tipo == Tipo.Azul) blueEffect.SetActive(true);
        else if (atacavel.tipo == Tipo.Verde) greenEffect.SetActive(true);
    }

    void FixedUpdate() {
        if (estado == Estado.Vagando) Vagar();
        else if (estado == Estado.Parado) Parado();
        else if (estado == Estado.Seguindo) Seguir();
        else if (estado == Estado.Fugindo) Fugir();
        
        agent.velocity = velocidade;
        transform.LookAt(transform.position);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioProcura);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward*0.5f, 0.5f);
    }

    void ProcurarAlvo() {
        // Procura por um alvo
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, raioProcura);
        
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].CompareTag("Player")) {
                alvo = hitColliders[i].gameObject;
                estado = Estado.Seguindo;
                break;
            }
        }
    }

    void Atacar() {
        // Procura por um alvo
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward * 0.5f, 0.5f);

        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].CompareTag("Player")) {
            }
        }
    }

    void Parado() {
        lastParadoTime += Time.fixedDeltaTime;

        if (lastParadoTime >= paradoTime) {
            lastParadoTime = 0;
            estado = Estado.Vagando;
            wanderTime = Random.Range(maxWanderTime * 0.5f, maxWanderTime);

            float angulo = Random.Range(0, 360);
            Vector3 direcao = Quaternion.AngleAxis(angulo, Vector3.up) * transform.forward;
            velocidade = direcao.normalized * speed;
        }

        ProcurarAlvo();
    }

    void Vagar() {
        lastWanderTime += Time.fixedDeltaTime;

        if (lastWanderTime >= wanderTime) {
            lastWanderTime = 0;
            estado = Estado.Parado;
            paradoTime = Random.Range(maxParadoTime * 0.25f, maxParadoTime);

            velocidade = Vector3.zero;
        }

        ProcurarAlvo();
    }

    void Seguir() {
        if (alvo == null) {
            estado = Estado.Vagando;
            return;
        }

        Vector3 desiredVel = (alvo.transform.position - transform.position).normalized * speed;
        Vector3 steering = desiredVel - velocidade;
        steering = steering.normalized * speed;
        velocidade = (velocidade + steering).normalized * speed;
    }

    void Fugir() {
        if (alvo == null) {
            estado = Estado.Vagando;
            return;
        }

        Vector3 desiredVel = (transform.position - alvo.transform.position).normalized * speed;
        Vector3 steering = desiredVel - velocidade;
        steering = steering.normalized * speed;
        velocidade = (velocidade + steering).normalized * speed;
    }
}
