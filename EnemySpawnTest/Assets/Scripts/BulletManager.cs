using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

	public GameObject bullet;
	public float movementSpeed = 10.0f;
	public float speed = 4.0f;
	public Rigidbody rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			var newBullet = Instantiate(bullet, GameObject.Find("BulletSpawner").transform.position, GameObject.Find("BulletSpawner").transform.rotation);
			newBullet.AddComponent<Bullet>();
		}
	}
}
