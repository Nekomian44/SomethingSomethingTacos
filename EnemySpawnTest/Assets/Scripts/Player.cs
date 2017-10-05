using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody rigid;
	public GameObject bullet;
	public float speed;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var newBullet = Instantiate(bullet, GameObject.Find("BulletSpawner").transform.position, GameObject.Find("BulletSpawner").transform.rotation);
			newBullet.AddComponent<Bullet>();
		}

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rigid.AddForce(movement * speed);
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Enemy")
		{
			Destroy(col.gameObject);
		}
	}
}
