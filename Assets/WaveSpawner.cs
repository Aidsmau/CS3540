using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    { 
        public GameObject[] enemyPrefabs;
        public int enemyCount;
        public float spawnRate;
       
    }
    
    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    public TMP_Text waveText;
    int currentWaveIndex = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentWaveIndex = PlayerPrefs.GetInt("LastWave", 0);
        StartCoroutine(ReleaseWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ReleaseWaves() {
        while(currentWaveIndex < waves.Length) {
            UpdateWaveText();
            yield return new WaitForSeconds(timeBetweenWaves);
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));

            //yield return new WaitUntil(AreAllEnemiesDead);
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
            currentWaveIndex++;

            PlayerPrefs.SetInt("LastWave", currentWaveIndex);
            PlayerPrefs.Save();
        }
    }

    IEnumerator SpawnWave(Wave wave) {
        for(int i = 0; i < wave.enemyCount; i++) {
            GameObject enemyPrefab = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];
            SpawnEnemy(enemyPrefab);
            yield return new WaitForSeconds(wave.spawnRate);

        }
    } 

    void SpawnEnemy(GameObject enemyPrefab) {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    bool AreAllEnemiesDead() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }

    void UpdateWaveText(){
        if(waveText){
            waveText.text = (currentWaveIndex + 1).ToString() + " / " + (waves.Length).ToString();
        }
    }

}

