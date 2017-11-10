using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;
	public float startDelay = 2f;
	public float spawnTime = 0.5f;
	public float numOfEnemies = 5;
	public Transform[] spawnPoints;

	public Text health_text;
	public Player player;

	public int totalEnemyCount = 0;
	public int numberOfEnemiesSpawned = 0;

	public float currentSpeed = -10.0f;

	void Start ()
	{
		InvokeRepeating ("SpawnEnemy", startDelay, spawnTime);
	}

	void SpawnEnemy ()
	{
		if (totalEnemyCount >= numOfEnemies || health_text.text == "GAME OVER")
			return;

		int spawnPointIndex = Random.Range(0, spawnPoints.Length);

		var enemy1 = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		enemy1.AddComponent<Enemy>().Init(this, currentSpeed, player);
		totalEnemyCount++;
	}

	public void OnEnemyDestroyed(EnemyCounter enemy)
	{
	}
}