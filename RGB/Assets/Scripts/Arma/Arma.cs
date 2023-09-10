using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma: MonoBehaviour {
    public float dano, cooldown, area;

    public abstract void Atacar();
    public abstract void AtacarEspecial();
}
