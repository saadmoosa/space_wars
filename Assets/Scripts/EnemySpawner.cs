using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //config params
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;
    [SerializeField] bool loop = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (loop);
        
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int i = 0; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int i = 0; i < waveConfig.NumberOfEnemies; i++)
        {
            //instantiate an enemy wave of this config and set the waveconfig into the enemypathing component
            var newEnemy = Instantiate(waveConfig.EnemyPrefab, waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
