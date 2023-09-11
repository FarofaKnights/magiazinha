using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    public static WaveController instance;

    public int wave = 0;
    public WaveInfo[] waves;

    public float spawnArea = 2;
    public GameObject inimigosHolder;

    public bool isWaveWaving = false;
    public bool waveAtStart = true;

    void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start() {
        if (waveAtStart)
            SpawnNewWave();
    }

    void FixedUpdate() { 
        if (isWaveWaving && inimigosHolder.transform.childCount == 0) {
            isWaveWaving = false;
            MagnetPower();
            SpawnNewWave();
        }
    }

    public void MagnetPower() {
        Power[] powers = FindObjectsOfType<Power>();

        foreach (Power power in powers) {
            power.target = GameManager.instance.player.transform.gameObject;
        }
    }

    public void SpawnNewWave() {
        if (isWaveWaving) return;

        wave = Random.Range(1, waves.Length);

        if (wave >= waves.Length) {
            wave = 0;
        }
        isWaveWaving = true;

        WaveInfo waveInfo = waves[wave-1];
        List<Transform> spawns = GetFreeSpawnArea();

        foreach (GameObject prefab in waveInfo.inimigosPrefab) {
            Transform spawn = GetRandomInList(spawns);

            GameObject inimigo = Instantiate(prefab, spawn.position, Quaternion.identity);
            inimigo.transform.SetParent(inimigosHolder.transform);
        }
    }

    Transform GetRandomInList(List<Transform> list) {
        int index = Random.Range(0, list.Count);
        return list[index];
    }

    List<Transform> GetFreeSpawnArea() {
        List<Transform> freeAreas = new List<Transform>();

        foreach (Transform child in transform) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, spawnArea);
            bool isFree = true;

            foreach (Collider collider in colliders) {
                if (collider.CompareTag("Player")) {
                    isFree = false;
                }
            }

            if (isFree) {
                freeAreas.Add(child);
            }
        }

        return freeAreas;
    }
}
