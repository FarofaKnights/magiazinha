using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject: MonoBehaviour {
    List<IObserver> list;
    public static Subject instance;

    void Start() {
        list = new List<IObserver>();
        instance = this;
    }

    public void Add(IObserver obs) {
        list.Add(obs);
    }

    public void NotifyAll() { 
        foreach (IObserver obs in list) {
            obs.QuemAvisa();
        }
    }
}
