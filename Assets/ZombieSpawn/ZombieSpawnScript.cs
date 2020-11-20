using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnScript : MonoBehaviour {
    public static GameObject[] spawnPositions = null;
    public static GameObject zombiesPackage = null;

    public GameObject[] prefabsToBeSpawn;

    public AnimationCurve spawnTime;

    public float spawnTimeMaxRate = 100;
    public float incrementalRate = 5;

    private void Awake() {
        if (spawnPositions == null) {
            spawnPositions = GameObject.FindGameObjectsWithTag("Respawn");
        }
        if (zombiesPackage == null) {
            zombiesPackage = GameObject.FindGameObjectWithTag("ZombiesPackage");
        }
    }

    void Start() {
//        InvokeRepeating("Spawn", 5, 5);
        StartCoroutine(ZombieSpawn());
    }

    private IEnumerator ZombieSpawn() {
        float time = 0;
        while (true) {
            yield return new WaitForSeconds(spawnTime.Evaluate(time/spawnTimeMaxRate));
            Spawn();
            if (time<spawnTimeMaxRate) {
                time+=incrementalRate;
            }
        }
    }

    void Spawn() {
        GameObject clone =  Instantiate(
            prefabsToBeSpawn[UnityEngine.Random.Range(0, prefabsToBeSpawn.Length - 1)],
            spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length - 1)].gameObject.transform.position,
            Quaternion.identity) as GameObject;

        if (zombiesPackage != null) {
            clone.transform.parent = zombiesPackage.transform;
        }
    }
}