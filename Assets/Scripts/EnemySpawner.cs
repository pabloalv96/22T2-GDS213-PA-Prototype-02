using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> wayConfigs;
    [SerializeField] float timeBetweenWaves = 0.5f;
    WaveConfigSO currentWave;
    [SerializeField] bool isLooping;

    void Start()
    {
        SpawnEnemyWaves();
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }


    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in wayConfigs)
            {
                currentWave = wave;

                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                    currentWave.GetStartingWavepoint().position,
                    Quaternion.Euler(0,0,180), transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawntime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping);
        

        
        
    }
}
