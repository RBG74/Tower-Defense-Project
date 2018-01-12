using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [Header("Unity setup")]
    public Transform enemyParent;
    public Transform spawnPoint;
    public Transform hotEnemyPrefab;
    public Transform coldEnemyPrefab;

    [Header("Spawn settings")]
    public float timeBeforeFirstWave = 2f;
    public float delayBetweenEnemies = 0.6f;
    public float delayBetweenWaves = 2f;
    public int waveNumber = 1;

    private float? countdown;
    private GameManager gameManager;
    private int nbOfEnemiesToSpawn;

    private void Awake()
    {
        countdown = timeBeforeFirstWave;
        gameManager = GameManager.instance;
    }

    void Update () {
        //A 0 on spawn une vague
		if(countdown <= 0)
        {
            nbOfEnemiesToSpawn = waveNumber + 9;
            gameManager.UpdatePercentages();
            gameManager.ResetDamageDone();
            var enemiesToSpawn = PrepareWave();
            StartCoroutine(SpawnWave(enemiesToSpawn));
            waveNumber++;
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

    private Transform[] PrepareWave()
    {
        float coldPercent = gameManager.coldPercent;
        float hotPercent = gameManager.hotPercent;
        Transform[] enemiesToSpawn = new Transform[nbOfEnemiesToSpawn];
        for (int x = 0; x < waveNumber + 9; x++)
        {
            var c = Mathf.RoundToInt(coldPercent / 100 * (nbOfEnemiesToSpawn));
            if (x < c)
                enemiesToSpawn[x] = coldEnemyPrefab;
            else
                enemiesToSpawn[x] = hotEnemyPrefab;
        }
        System.Random rnd = new System.Random();
        enemiesToSpawn = enemiesToSpawn.OrderBy(x => rnd.Next()).ToArray();
        return enemiesToSpawn;
    }

    private IEnumerator SpawnWave(Transform[] enemiesToSpawn)
    {
        for (int i = 1; i < nbOfEnemiesToSpawn; i++)
        {
            SpawnEnemy(enemiesToSpawn[i]);

            if(i < nbOfEnemiesToSpawn)
            {
                yield return new WaitForSeconds(delayBetweenEnemies);
            }
        }
    }

    private void SpawnEnemy(Transform enemyPrefab)
    {
        var enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.parent = enemyParent;
    }
}
