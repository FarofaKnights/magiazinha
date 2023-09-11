using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girar : MonoBehaviour{
    public Vector3 eixo;
    public float speed;

    void FixedUpdate(){
        transform.Rotate(eixo * Time.deltaTime * speed);
        
    }
}
