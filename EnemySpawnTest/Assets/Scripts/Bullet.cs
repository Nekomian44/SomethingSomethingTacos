using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 2.0f;
	private Rigidbody rigid;
	public float movementSpeed = 5.0f;

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
			Destroy (this.gameObject);
		}
	}
}
