using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;
	public float startDelay = 5f;
	public float spawnTime = 3f;
	public float numOfEnemies = 5;
	public Transform[] spawnPoints;

	public int totalEnemyCount = 0;

	void Start ()
	{
		InvokeRepeating ("SpawnEnemy", startDelay, spawnTime);
	}

	void SpawnEnemy ()
	{
		if (totalEnemyCount >= numOfEnemies)
			return;

		int spawnPointIndex = Random.Range(0, spawnPoints.Length);

		var enemy1 = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		enemy1.AddComponent<Enemy>().Init(this);
		totalEnemyCount++;
	}

	public void OnEnemyDestroyed(EnemyCounter enemy)
	{
		totalEnemyCount--;
	}
}