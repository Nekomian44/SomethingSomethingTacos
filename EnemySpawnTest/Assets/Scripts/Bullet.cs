using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 2.0f;
	private Rigidbody rigid;
	public float movementSpeed = 5.0f;
	private Player player;

	public void Init(Player play)
	{
		player = play;
		this.Start();
	}

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3(0.0f, 0.0f, movementSpeed);

		rigid.AddForce(movement * speed);

		if (transform.position.z >= 40)
			Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Enemy")
		{
			Destroy(col.gameObject);
			Destroy(this.gameObject);
			player.AddToScore(true);
		}
		if(col.gameObject.name == "Invincibility")
		{
			Destroy(col.gameObject);
			Destroy(this.gameObject);
			player.Invincible();
		}
		if(col.gameObject.name == "Shield")
		{
			Destroy(col.gameObject);
			Destroy(this.gameObject);
			player.Shield();
		}
		if (col.gameObject.name == "RapidFire")
		{
			Destroy(col.gameObject);
			Destroy(this.gameObject);
			player.RapidFire();
		}
	}
}
