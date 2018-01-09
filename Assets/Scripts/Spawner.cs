using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [Header("Unity setup")]
    public Transform enemyParent;
    public Transform spawnPoint;
    public Transform enemyPrefab;

    [Header("Spawn settings")]
    public float timeBeforeFirstWave = 2f;
    public float delayBetweenEnemies = 0.6f;
    public float delayBetweenWaves = 2f;
    public int waveNumber = 1;

    private float? countdown;

    private void Awake()
    {
        countdown = timeBeforeFirstWave;
    }

    void Update () {
        //A 0 on spawn une vague
		if(countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = null;
        }

        //Si countdown n'est pas null on le décrémente
        if (countdown != null)
        {
            countdown -= Time.deltaTime;
        }
        //Si countdown est null et qu'il n y a plus d'enemies on réinitialise countdown
        else if (enemyParent.childCount == 0)
        {
            countdown = delayBetweenWaves;
        }
	}

    private IEnumerator SpawnWave()
    {
        for (int i = 1; i <= waveNumber; i++)
        {
            SpawnEnemy();
            if(i < waveNumber)
            {
                yield return new WaitForSeconds(delayBetweenEnemies);
            }
        }

        waveNumber++;
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.parent = enemyParent;
    }
}
