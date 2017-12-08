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
	public AudioSource playerAudio, background;
	public AudioClip crash1, crash2, crash3, crash4, blackHole;
	public float restartDelay = 16f;
	private float restartTimer, moveHorizontal, moveVertical, extraX = 0, extraY = 0, extraZ = 0;
	private bool destroyed = false, damaged = false, flashSwitcher = true;
	private int damageDelay, damageDelayLimit = 20, damageFlashDelay, damageDelayFlashLimit = 50, invincibleTime = 0, rapidFireTime = 0;
	private bool rapidfire = false, invincible = false, shielded = false;

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
		if (!destroyed)
		{
			if (Input.GetMouseButtonDown(0) && !rapidfire)
			{
				var newBullet = Instantiate(bullet, GameObject.Find("BulletSpawner").transform.position, GameObject.Find("BulletSpawner").transform.rotation);
				newBullet.AddComponent<Bullet>().Init(this);
			}

			if(invincibleTime <= 0)
			{
				invincible = false;
				rigid.detectCollisions = true;
			}

			if(rapidfire)
			{
				if (rapidFireTime <= 0)
					rapidfire = false;
				var newBullet = Instantiate(bullet, GameObject.Find("BulletSpawner").transform.position, GameObject.Find("BulletSpawner").transform.rotation);
				newBullet.AddComponent<Bullet>().Init(this);
			}

			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");

			Vector3 movement;

			if (GameObject.Find("Player").transform.position.z >= -9)
			{
				movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
				if (GameObject.Find("Player").transform.position.z > -9)
					GameObject.Find("Player").transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, -9.0f);
			}
			else
				movement = new Vector3(moveHorizontal, moveVertical, 0.03f);

			int life = (int)(((-12.6701 - GameObject.Find("Player").transform.position.z) / (-3.6701)) * 100) + 1;

			health.text = "Integrity: " + life + "%";

			rigid.AddForce(movement * speed);

			if(damaged)
			{
				health.color = Color.red;
				damageDelay++;
			}
			if (damageDelay >= damageDelayLimit)
			{
				health.color = Color.white;
				damaged = false;
				damageDelay = 0;
				System.Console.WriteLine("reset");
			}
			if(life <= 25)
			{
				damageFlashDelay++;
				Debug.Log(damageFlashDelay);
				if(damageFlashDelay >= damageDelayFlashLimit && flashSwitcher)
				{
					health.color = Color.red;
					damageFlashDelay = 0;
					damaged = false;
					flashSwitcher = false;
					System.Console.WriteLine("red");
				}
				else if(damageFlashDelay >= damageDelayFlashLimit && !flashSwitcher)
				{
					health.color = Color.white;
					damageFlashDelay = 0;
					damaged = false;
					flashSwitcher = true;
					System.Console.WriteLine("white");
				}
			}
			else if(life >= 25 && !damaged)
			{
				health.color = Color.white;
			}
		}
		else
		{
			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay)
				SceneManager.LoadScene("mainMenu");
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Enemy")
		{
			Destroy(col.gameObject);
			Hit();
		}
		if(col.gameObject.name == "Despawner")
		{
			GameOver();
			GameObject.Find("Player").transform.position = new Vector3(-100f, -100f, -100f);
			CancelInvoke();
			health.text = "GAME OVER";
		}
	}

	public void Invincible()
	{
		invincible = true;
		invincibleTime = 15;
		rigid.detectCollisions = false;
	}

	public void Shield()
	{
		shielded = true;
	}

	public void RapidFire()
	{
		rapidfire = true;
		rapidFireTime = 10;
	}

	void Hit()
	{
		switch (UnityEngine.Random.Range(0, 4))
		{
			case 0:
				playerAudio.PlayOneShot(crash1);
				break;
			case 1:
				playerAudio.PlayOneShot(crash2);
				break;
			case 2:
				playerAudio.PlayOneShot(crash3);
				break;
			default:
				playerAudio.PlayOneShot(crash4);
				break;
		}
		damaged = true;
	}

	void GameOver()
	{
		background.loop = false;
		background.clip = blackHole;
		background.Play();
		destroyed = true;
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
		if (invincibleTime >= 0)
			invincibleTime--;
		if (rapidFireTime >= 0)
			rapidFireTime--;
	}

	void OnDestroy()
	{
		destroyed = true;
	}
}
