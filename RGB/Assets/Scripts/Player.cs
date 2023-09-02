using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    CharacterController controller;
    GameObject corpo;

    public float speed = 5f;

    public Arma arma;

    void Start() {
        controller = GetComponent<CharacterController>();
        corpo = transform.Find("Corpo").gameObject;

        SelectArma("Cajado");
    }

    void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, y);
        controller.Move(move * Time.deltaTime * speed);


        // Rotacionar jogador
        Vector3 mousePosition = GetMousePosition();

        if (mousePosition != Vector3.zero) {
            corpo.transform.LookAt(mousePosition);
        }

        // Ataque
        if (Input.GetMouseButtonDown(0)) {
            arma.Atacar();
        }
    }


    Vector3 GetMousePosition() {
        Vector3 pos = Vector3.zero;

        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;
        if (plane.Raycast(ray, out distance)) {
            pos = ray.GetPoint(distance);
        }

        return pos;
    }

    public void SelectArma(string nome) {
        if (arma != null) {
            MonoBehaviour script = (MonoBehaviour)arma;
            script.gameObject.SetActive(false);
        }

        GameObject armaObj = corpo.transform.Find(nome).gameObject;
        armaObj.SetActive(true);
        arma = armaObj.GetComponent<Arma>();
    }
}
