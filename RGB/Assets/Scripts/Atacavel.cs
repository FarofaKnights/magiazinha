using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tipo { Neutro, Vermelho, Verde, Azul }

public class Atacavel : MonoBehaviour {
    public float vida, vidaMaxima;
    public Tipo tipo = Tipo.Neutro;
    public bool sobrecarregado = false;

    float MultiplicadorDano(Tipo danoRecebido) {
        if (tipo == Tipo.Vermelho) {
            if (danoRecebido == Tipo.Azul) return 2;
            if (danoRecebido == Tipo.Verde) return 0.5f;
        } else if (tipo == Tipo.Verde) {
            if (danoRecebido == Tipo.Vermelho) return 2;
            if (danoRecebido == Tipo.Azul) return 0.5f;
        } else if (tipo == Tipo.Azul) {
            if (danoRecebido == Tipo.Verde) return 2;
            if (danoRecebido == Tipo.Vermelho) return 0.5f;
        }

        return 1;
    }

    public void SofrerDano(float dano, Tipo tipoDano = Tipo.Neutro) {
        float danoReal = dano * MultiplicadorDano(tipoDano);

        vida -= danoReal;

        if (vida <= 0) {
            Morrer();
        }
    }

    public virtual void Morrer() {
        Destroy(gameObject);
    }

    public void GerarTipoAleatorio() {
        Tipo[] tipos = { Tipo.Vermelho, Tipo.Verde, Tipo.Azul };
        int index = Random.Range(0, tipos.Length);
        tipo = tipos[index];
    }
}
