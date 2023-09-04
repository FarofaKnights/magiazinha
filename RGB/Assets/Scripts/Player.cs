using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    CharacterController controller;
    GameObject corpo;

    public float speed = 5f;
    bool isDashing = false;
    public float dashSpeed = 10f, dashTime = 0.2f;

    public Arma arma;

    void Start() {
        controller = GetComponent<CharacterController>();
        corpo = transform.Find("Corpo").gameObject;

        SelectArma("Espada");
    }

    void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, y);
        controller.SimpleMove(move * speed);


        // Rotacionar jogador
        Vector3 mousePosition = GetMousePosition();

        if (mousePosition != Vector3.zero) {
            corpo.transform.LookAt(mousePosition);
        }

        if (arma != null) {
            // Ataque
            if (Input.GetMouseButtonDown(0)) {
                arma.Atacar();
            } else if (Input.GetMouseButtonDown(1)) {
                arma.AtacarEspecial();
            }
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing) {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash() {
        isDashing = true;

        float dashTimer = 0f;

        Vector3 dashDirection = corpo.transform.forward;
        dashDirection.y = 0;

        while (dashTimer < dashTime) {
            dashTimer += Time.deltaTime;
            controller.Move(dashDirection * Time.deltaTime * dashSpeed);
            yield return null;
        }

        isDashing = false;
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

    public string GetArma() {
        if (arma == null) return "";

        MonoBehaviour script = (MonoBehaviour)arma;
        return script.gameObject.name;
    }
}
