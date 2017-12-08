using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour {

	public void Init()
	{
		this.Start();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3(0, 0, -5.0f);
		transform.Translate(movement * Time.deltaTime);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Despawner")
		{
			Destroy(this);
		}
	}
}
