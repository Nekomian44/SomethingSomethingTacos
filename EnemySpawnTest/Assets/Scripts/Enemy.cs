using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private GameObject invincibility, rapidfire, shield;
    public int health = 10, currentLevel;
	public float speed = 4;
	private Rigidbody rigid;
	private EnemyManager _manager;
	public float movementSpeed = -10.0f;
	private Player _player;
	private bool despawned = false, first = true;
	private float movementX, movementY;

	public void Init(EnemyManager manager, float currentSpeed, Player player, int level, GameObject passedInvincibility,
		GameObject passedRapidFire, GameObject passedShield)
	{
		_manager = manager;
		_player = player;
		this.name = "Enemy";
		this.Start();
		movementSpeed = currentSpeed;
		currentLevel = level;
		movementX = UnityEngine.Random.Range(-currentLevel, currentLevel);
		movementY = UnityEngine.Random.Range(-currentLevel, currentLevel);
		invincibility = passedInvincibility;
		rapidfire = passedRapidFire;
		shield = passedShield;
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
		
		Vector3 movement = new Vector3(movementX, movementY, movementSpeed);

		rigid.AddForce(movement * speed);
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Despawner")
		{
			despawned = true;
			Destroy(this.gameObject);
			_player.AddToScore(false);
		}
		if (col.gameObject.name == "PlayerBoundary - Right" || col.gameObject.name == "PlayerBoundary - Left")
			movementX = -movementX;
		if (col.gameObject.name == "PlayerBoundary - Top" || col.gameObject.name == "Floor")
			movementY = -movementY;
	}

    void OnDestroy()
    {
		_manager.numberOfEnemiesSpawned++;
		if (_manager.currentSpeed > -30.0f && !despawned)
		{
			_manager.enemiesLeft -= 1;
			_manager.UpdateKills(true, transform.position);
			_manager.currentSpeed -= 0.1f;
		}
    }
}
