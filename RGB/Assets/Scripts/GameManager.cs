using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public Player player;

    public LayerMask layerInimigo;

    public int power = 0, maxPower = 30;

    public string[] weapons = { "Espada", "Soco", "Cajado" };
    int currentWeapon = 0;

    public bool winning = false;

    void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddPower(int amount = 1) {
        power += amount;
        UIController.instance.UpdatePowerText();

        if (power >= maxPower) {
            Win();
        }
    }

    public void Win() {
        winning = true;
        SceneManager.LoadScene("Win");
    }

    public void Lose() {
        SceneManager.LoadScene("Lose");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            currentWeapon = (currentWeapon + 1) % weapons.Length;
            player.SelectArma(weapons[currentWeapon]);
        }
    }
}
