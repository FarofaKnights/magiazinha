using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {
    void Update() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (Collider c in colliders) {
                c.SendMessage("QuemAvisa");
            }
        }
    }
}
