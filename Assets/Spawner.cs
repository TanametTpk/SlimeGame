using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameObject template;
    [SerializeField] public int limit;
    [SerializeField] private bool isSpawnOnce;
    [SerializeField] private bool isDestroyAfterFinish;
    [SerializeField] private float delay;
    private float nextSpawnAt = 0;
    private int currentSpawned = 0;

    private void Start() {
        SpawnAll();
    }

    private void Update() {
        if (currentSpawned >= limit) {
            DestorySelf();
            return;
        }
        
        if (nextSpawnAt <= 0) {
            Spawn();
            nextSpawnAt = delay;
            currentSpawned += 1;
        }

        nextSpawnAt -= Time.deltaTime;
    }

    public void SpawnAll() {
        if (isSpawnOnce) {
            for (int i = 0; i < limit; i++)
            {
                Spawn();
            }
            DestorySelf();
        }
    }

    public void Spawn() {
        Instantiate(template, transform.position, Quaternion.identity);
    }

    public void ResetSpawn() {
        nextSpawnAt = 0;
        currentSpawned = 0;
        SpawnAll();
    }

    private void DestorySelf() {
        if (isDestroyAfterFinish) {
            Destroy(this.gameObject);
        }
    }
}
