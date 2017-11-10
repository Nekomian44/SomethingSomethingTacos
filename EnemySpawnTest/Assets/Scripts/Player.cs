using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	private Rigidbody rigid;
	public GameObject bullet;
	public float speed;
	public Text health, score, time;
	public int maxHealth;
	public int currentHealth, currentScore = 0, currentMinutes = 0, currentSeconds = 0;
	public AudioClip crash1, crash2, crash3, crash4;
	private bool destroyed = false;

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

		if (GameObject.Find("Player").transform.position.z >= -9)
		{
			movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
			if(GameObject.Find("Player").transform.position.z > -9)
				GameObject.Find("Player").transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, -9.0f);
		}
		else
			movement = new Vector3(moveHorizontal, moveVertical, 0.03f);

		int life = (int)(((-12.6701 - GameObject.Find("Player").transform.position.z) / (-3.6701)) * 100) + 1;

		health.text = "Integrity: " + life + "%";

		rigid.AddForce(movement * speed);
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Enemy")
		{
			Destroy(col.gameObject);
			rigid.AddForce(new Vector3(0, 0, -20f));
		}
		if(col.gameObject.name == "Despawner")
		{
			Destroy(this.gameObject);
			health.text = "GAME OVER";
			SceneManager.LoadScene("mainMenu");
		}
	}

	public void AddToScore(bool hit)
	{
		if (!destroyed)
		{
			if (hit)
				currentScore += 100;
			else
				currentScore += 25;
			score.text = "Score: " + currentScore;
		}
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

	void OnDestroy()
	{
		destroyed = true;
	}
}
