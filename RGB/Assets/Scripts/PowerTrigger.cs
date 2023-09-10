using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameManager.instance.AddPower();
            Destroy(gameObject);
        }
    }
}
