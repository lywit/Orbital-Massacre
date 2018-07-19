using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [Header("Object Assignment")]
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    GameObject winScreen;
    [SerializeField]
    Transform enemyHolder;
    [SerializeField]
    GameManager gamemanager;

	[Header("Waves")]
	[SerializeField]
	int[] enemyCount;
	[SerializeField]
	float[] enemyDistance;
	[SerializeField]
	float[] waveDelay;
    [SerializeField]
    GameObject[] enemyType;

    [Header("Misc")]
    [SerializeField]
    float updateDelay = .5f;
    [SerializeField]
	int currentWave = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine (waveLoop ());
	}
	
	IEnumerator waveLoop()
	{
		while (currentWave != enemyCount.Length) {
			StartCoroutine (SpawnWave(enemyCount[currentWave], enemyDistance[currentWave]));
			yield return new WaitForSeconds (waveDelay[currentWave]);
		}
        while (enemyHolder.childCount > 0)
        {
            yield return new WaitForSeconds(updateDelay);
        }
        gamemanager.GameWon();
    }

    IEnumerator SpawnWave(int enemyCount, float enemyDistance)
    {

        Vector3 spawnPosition;
        int nextSpawner = 0;

        for (int i = 0; i < enemyCount; i++)
        {
            spawnPosition = spawnPoints[nextSpawner].position;

            if (nextSpawner + 1 >= spawnPoints.Length)
                nextSpawner = 0;
            else
                nextSpawner++;

            GameObject spawnedEnemy = (GameObject)Instantiate(enemyType[currentWave], spawnPosition, Quaternion.identity);
            spawnedEnemy.transform.parent = enemyHolder;
            yield return new WaitForSeconds(enemyDistance);
        }
		currentWave++;
    }
}
