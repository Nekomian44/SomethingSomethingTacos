using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 10;
	public float speed = 4;
	private Rigidbody rigid;
	private EnemyManager _manager;
	public float movementSpeed = -10.0f;
	private Player _player;

	public void Init(EnemyManager manager, float currentSpeed, Player player)
	{
		_manager = manager;
		_player = player;
		this.name = "Enemy";
		this.Start();
		movementSpeed = currentSpeed;
	}

	// Use this for initialization
	void Start () 
	{
		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (health == 0)
            Destroy(this.gameObject);

		//float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;

		Vector3 movement = new Vector3(0.0f, 0.0f, movementSpeed);

		rigid.AddForce(movement * speed);
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Despawner")
		{
			Destroy(this.gameObject);
			_player.AddToScore(false);
		}
	}

    void OnDestroy()
    {
        _manager.totalEnemyCount--;
		_manager.numberOfEnemiesSpawned++;
		if(_manager.currentSpeed > -30.0f)
			_manager.currentSpeed -= 0.1f;
    }
}
