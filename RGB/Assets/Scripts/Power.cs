using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour {
    public GameObject target;
    public float speed;
    
    void FixedUpdate() {
        if (target != null) {
            Vector3 direction = target.transform.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }
}
