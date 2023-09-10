using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjetil : MonoBehaviour {
    public Vector3 velocity;
    public float speed;

    void Start () {
        Destroy(gameObject, 5f);
    }

    void FixedUpdate() {
        transform.position += velocity * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Atacavel>().SofrerDano(1, Tipo.Vermelho);
        }

        Destroy(gameObject);
    }
}
