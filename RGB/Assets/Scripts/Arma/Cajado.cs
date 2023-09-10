using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cajado : Arma {
    Animator animator;

    public GameObject projetilPrefab;
    public Transform spawnArea;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public override void Atacar() {
        animator.SetTrigger("Magia");

        GameObject projetil = Instantiate(projetilPrefab, spawnArea.position, spawnArea.rotation);
    }

    public override void AtacarEspecial() {
        animator.SetTrigger("Especial");

        
    }
}
