using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    public int wave = 0;
    public WaveInfo[] waves;

    public float spawnArea = 2;
    public GameObject inimigosHolder;

    public bool isWaveWaving = false;

    void Start() {
        SpawnNewWave();
    }

    void FixedUpdate() { 
        if (isWaveWaving && inimigosHolder.transform.childCount == 0) {
            isWaveWaving = false;

            Invoke("SpawnNewWave", 3);
        }
    }

    public void SpawnNewWave() {
        if (isWaveWaving) return;
        if (wave >= waves.Length) {
            wave = 0;
        }
        isWaveWaving = true;

        wave++;
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
