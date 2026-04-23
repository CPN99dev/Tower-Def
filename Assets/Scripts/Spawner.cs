using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint;

    public void StartWave(WaveData wave)
    {
        StartCoroutine(SpawnWave(wave));
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(wave.rate);
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}