using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soco : MonoBehaviour, Arma {
    Animator animator;

    public float dano = 10f;
    public float area = 1f;

    public Transform danoArea;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public void Atacar() {
        animator.SetTrigger("Soco");

        Collider[] colliders = Physics.OverlapSphere(danoArea.position, area);

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

    public void AtacarEspecial() {
        animator.SetTrigger("Especial");
    }
}
