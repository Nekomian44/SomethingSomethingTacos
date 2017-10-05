using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
	private EnemyManager _manager;

	public int health = 10;

	public void Init(EnemyManager manager)
	{
		_manager = manager;
		this.name = "Enemy";
	}

	public void Update () 
	{
		if (health <= 0)
			Destroy(this.gameObject);

		//float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
		//float z = Time.deltaTime * 1.5f;

		//transform.Rotate(0, x, 0);
		//transform.Translate(0, 0, 0-z);
	}

	public void OnDestroy()
	{
		_manager.OnEnemyDestroyed(this);
	}
}