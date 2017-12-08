using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

	public GameObject player;
	public Text health_text;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
		if (health_text.text == "GAME OVER")
			transform.position = new Vector3(-0.031f, 1.64f, -11.41f);
	}
}
