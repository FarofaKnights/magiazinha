using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soco : Arma {
    Animator animator;

    public Transform danoArea;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public override void Atacar() {
        animator.SetTrigger("Soco");

        Collider[] colliders = Physics.OverlapSphere(danoArea.position, area, GameManager.instance.layerInimigo);

        foreach (Collider collider in colliders) {
            Atacavel atacavel = collider.GetComponent<Atacavel>();

            if (atacavel != null) {
                atacavel.SofrerDano(dano, Tipo.Verde);
            }
        }
    }

    //gizmos
    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(danoArea.position, area);
    }

    public override void AtacarEspecial() {
        animator.SetTrigger("Especial");
    }
}
