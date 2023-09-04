using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject bluePowerUpPrefab, redPowerUpPrefab, greenPowerUpPrefab;

    public static GameManager instance;

    public GameObject redSpawn, blueSpawn, greenSpawn;

    public Player player;

    void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start() {
        InvokeRepeating("CheckEmpty", 0, 0.2f);
    }

    void CheckEmpty() {
        string playerWeapon = player.GetArma();

        if (redSpawn.transform.childCount == 0 && playerWeapon != "Cajado") {
            GameObject powerUp = Instantiate(redPowerUpPrefab, redSpawn.transform.position, Quaternion.identity);
            powerUp.transform.parent = redSpawn.transform;
        }

        if (blueSpawn.transform.childCount == 0 && playerWeapon != "Espada") {
            GameObject powerUp = Instantiate(bluePowerUpPrefab, blueSpawn.transform.position, Quaternion.identity);
            powerUp.transform.parent = blueSpawn.transform;
        }

        if (greenSpawn.transform.childCount == 0 && playerWeapon != "Soco") {
            GameObject powerUp = Instantiate(greenPowerUpPrefab, greenSpawn.transform.position, Quaternion.identity);
            powerUp.transform.parent = greenSpawn.transform;
        }
    }
}
