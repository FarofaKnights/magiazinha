using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini_RSimples : AtaqueBehaviour {
    public float carregarTimer;
    float currentCarregarTimer = 0;

    GameObject target;
    Inimigo inimigo;

    public GameObject projetilPrefab;
    public GameObject[] projetilSpawnPoints;

    void Start() {
        inimigo = GetComponent<Inimigo>();
        inimigo.ataque = this;
    }

    public override bool Atacando(GameObject target) {
        this.target = target;

        if (currentCarregarTimer < carregarTimer) {
            currentCarregarTimer += Time.deltaTime;
        } else {
            currentCarregarTimer = 0;
            Atacar();
            return false;
        }

        return true;
    }

    public void Atacar() {
        foreach (GameObject spawnPoint in projetilSpawnPoints) {
            GameObject projetil = Instantiate(projetilPrefab, spawnPoint.transform.position, Quaternion.identity);
            EnemyProjetil enemyProjetil = projetil.GetComponent<EnemyProjetil>();
            enemyProjetil.velocity = (target.transform.position - spawnPoint.transform.position).normalized;
        }
    }
}
