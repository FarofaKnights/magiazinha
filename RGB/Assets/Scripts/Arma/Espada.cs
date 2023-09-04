using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour, Arma {
    Animator animator;

    public float dano = 15f;
    public float area = 3f;

    public Transform danoArea;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public void Atacar() {
        animator.SetTrigger("Ataque");

        Collider[] colliders = Physics.OverlapSphere(danoArea.position, area);

        foreach (Collider collider in colliders) {
            Atacavel atacavel = collider.GetComponent<Atacavel>();

            if (atacavel != null) {
                atacavel.SofrerDano(dano, Tipo.Azul);
            }
        }
    }

    //gizmos
    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(danoArea.position, area);
    }

    public void AtacarEspecial() {
        animator.SetTrigger("Especial");
    }
}
