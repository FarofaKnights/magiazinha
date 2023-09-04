using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IObserver{
    // Start is called before the first frame update
    void Start() {
        Subject.instance.Add(this);
    }

    public void QuemAvisa() {
        GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log(gameObject.name + " ouviu");
    }
}
