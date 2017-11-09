using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	private Rigidbody rigid;
	public GameObject bullet;
	public float speed;
	public Text health, score, time;
	public int maxHealth;
	public int currentHealth, currentScore = 0, currentMinutes = 0, currentSeconds = 0;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();
		currentHealth = maxHealth;
		health.text = "";
		score.text = "Score: " + currentScore;
		print(string.Format("00", currentMinutes) + ":" + string.Format("00", currentSeconds));
		time.text = "Time: " + currentMinutes.ToString("00") + ":" + currentSeconds.ToString("00");
		InvokeRepeating("TimerCount", 1.0f, 1.0f);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var newBullet = Instantiate(bullet, GameObject.Find("BulletSpawner").transform.position, GameObject.Find("BulletSpawner").transform.rotation);
			newBullet.AddComponent<Bullet>().Init(this);
		}

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement;

		if (GameObject.Find("Player").transform.position.z >= -9.5f)
			movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
		else
			movement = new Vector3(moveHorizontal, moveVertical, 0.02f);

		rigid.AddForce(movement * speed);
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Enemy")
		{
			Destroy(col.gameObject);
		}

		if(col.gameObject.name == "PlayerBoundary - Back")
		{
			Destroy(this.gameObject);
			health.text = "GAME OVER";
		}
	}

	public void AddToScore()
	{
		currentScore += 50;
		score.text = "Score: " + currentScore;
	}

	void TimerCount()
	{
		currentSeconds++;
		if(currentSeconds >= 60)
		{
			currentSeconds = 0;
			currentMinutes++;
		}
		time.text = "Time: " + currentMinutes.ToString("00") + ":" + currentSeconds.ToString("00");
	}
}
