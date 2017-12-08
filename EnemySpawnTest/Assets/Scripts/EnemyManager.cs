using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy, invincibility, rapidFire, shield;
	public float startDelay = 5f;
	public float spawnTime = 1.0f;
	public float numOfEnemies = 5;
	public Transform[] spawnPoints;

	public Text health_text, level_text, kills_text;
	public Player player;

	public int totalEnemyCount = 0;
	public int numberOfEnemiesSpawned = 0;

	public float currentSpeed = -10.0f;
	public int currentLevel = 1;
	public int enemiesLeft = 5;

	void Start ()
	{
		InvokeRepeating ("SpawnEnemy", startDelay, spawnTime);
	}

	void SpawnEnemy ()
	{
		if (health_text.text == "GAME OVER")
			return;

		int spawnPointIndex = Random.Range(0, spawnPoints.Length);

		var enemy1 = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		enemy1.AddComponent<Enemy>().Init(this, currentSpeed, player, currentLevel, invincibility, rapidFire, shield);

		if(enemiesLeft <= 0)
		{
			currentLevel++;
			enemiesLeft = currentLevel * 5;
			CancelInvoke();
			spawnTime = spawnTime * .8f;
			InvokeRepeating("SpawnEnemy", startDelay, spawnTime);
			level_text.text = "Level: " + currentLevel;
			Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
			UpdateKills(false, position);
		}

		totalEnemyCount++;
	}

	public void UpdateKills(bool fromEnemy, Vector3 position)
	{
		kills_text.text = "Kills Needed: " + enemiesLeft;
		if (UnityEngine.Random.Range(0, 100) <= 5 && fromEnemy)
		{
			switch (UnityEngine.Random.Range(0, 2))
			{
				case 0:
					player.Invincible();
					break;
				case 1:
					player.RapidFire();
					break;
			}
		}
	}

	public void OnEnemyDestroyed(EnemyCounter enemy)
	{
	}
}