using UnityEngine;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int maxEnemyCount = 20;
    void Start()
    {
        if (!enemyPrefab)
        {
            return;
        }
        //InvokeRepeating("SpawnEnemy", 2, 3);
        Debug.Log("coroutine started " + Time.time);
        StartCoroutine(SpawnEnemies(2));
    }

    void SpawnEnemy()
    {
        var positionOffset = Random.insideUnitSphere * 5;
        Instantiate(enemyPrefab, transform.position + positionOffset, transform.rotation);
    }

    IEnumerator SpawnEnemies(float spawnInterval)
    {
        Debug.Log("before yield " + Time.time);
        while (true)
        {
            var enemyCount = GameObject.FindGameObjectsWithTag("Dementor").Length;
            if(enemyCount < maxEnemyCount)
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
            Debug.Log("after yield " + Time.time);
        }
        
        
    }
}