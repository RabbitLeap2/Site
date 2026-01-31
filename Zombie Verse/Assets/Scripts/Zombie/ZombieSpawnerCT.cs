using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawnerCT : MonoBehaviour
{
    public int initialZombiePerWave = 5;
    public int currentZombiePerWave;

    public float spawnDelay = 0.5f;

    public int currentWave = 0;
    public float waveCooldown = 10.0f;

    public bool inCooldown;
    public float cooldownCounter = 0;
    public GameObject zombiePrefab;
    public Transform player; // arrasta o Player no Inspector
    public float spawnDistance = 20f;  // distância para spawnar zumbis
    public float despawnDistance = 30f; // distância para despawnar

    public List<Zombie> currentZombiesAlive;

    private void Start()
    {
        currentZombiePerWave = initialZombiePerWave;

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiePerWave; i++)
        {
            // Gera posição aleatória em volta do spawner
            Vector3 spawnOffset = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Cria zumbi sempre
            GameObject zombieGO = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            Zombie zombieScript = zombieGO.GetComponent<Zombie>();
            currentZombiesAlive.Add(zombieScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }


    private void Update()
    {
        List<Zombie> zombiesToRemove = new List<Zombie>();

        foreach (Zombie zombie in currentZombiesAlive)
        {
            float distance = Vector3.Distance(player.position, zombie.transform.position);

            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
            }
            else if (distance > despawnDistance)
            {
                // Em vez de destruir, podes desativar para não perder referência
                zombie.gameObject.SetActive(false);
            }
            else
            {
                zombie.gameObject.SetActive(true); // volta a ativar se o player se aproximar
            }
        }


        foreach (Zombie zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        if (currentZombiesAlive.Count == 0 && !inCooldown)
        {
            StartCoroutine(waveCoolDown());
        }


        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }
    }


    private IEnumerator waveCoolDown()
    {
        inCooldown = true;

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;

        currentZombiePerWave *= 2;

        StartNextWave();
    }
}
