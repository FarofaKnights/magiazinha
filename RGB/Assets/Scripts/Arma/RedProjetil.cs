using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProjetil : MonoBehaviour {
    public float velocidade = 10f;
    public LayerMask layerMask;

    void Start() {
        Destroy(gameObject, 5f);
    }

    void FixedUpdate() {
        transform.Translate(Vector3.forward * velocidade * Time.fixedDeltaTime);

        // Check for radius sphere collision
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.75f, layerMask);

        foreach (Collider collider in colliders) {
            Atacavel atacavel = collider.gameObject.GetComponent<Atacavel>();

            if (atacavel != null) {
                atacavel.SofrerDano(10f, Tipo.Vermelho);
            }

            Destroy(gameObject);
        }
    }
}
