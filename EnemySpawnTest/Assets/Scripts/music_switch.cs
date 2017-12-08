using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_switch : MonoBehaviour {

	private AudioSource aud;
	public AudioClip[] songs;
	private int selection;

	// Use this for initialization
	void Start () {
		aud = this.GetComponent<AudioSource> ();
		selection = Random.Range (0, songs.Length);
		aud.clip = songs [selection];
	}
	
	// Update is called once per frame
	void Update () {
		if (!aud.isPlaying && aud != null) {
			aud.Play ();
		}
	}

	void OnDestroy()
	{
		Destroy(this);
	}
}
