using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProjetil : MonoBehaviour {
    public float velocidade = 10f;

    void FixedUpdate() {
        transform.Translate(Vector3.forward * velocidade * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision) {
        Atacavel atacavel = collision.gameObject.GetComponent<Atacavel>();

        if (atacavel != null) {
            atacavel.SofrerDano(10f, Tipo.Vermelho);
        }

        Destroy(gameObject);
    }
}
