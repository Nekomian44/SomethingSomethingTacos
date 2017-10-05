using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 10;
	public float speed = 4;
	private Rigidbody rigid;
	private EnemyManager _manager;
	public float movementSpeed = -10.0f;

	public void Init(EnemyManager manager)
	{
		_manager = manager;
		this.name = "Enemy";
		this.Start();
	}

	// Use this for initialization
	void Start () 
	{
		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        if (health == 0)
            Destroy(this.gameObject);

		//float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;

		Vector3 movement = new Vector3(0.0f, 0.0f, movementSpeed);

		rigid.AddForce(movement * speed);

		if (transform.position.z <= -20)
			Destroy(this.gameObject);
	}
}
