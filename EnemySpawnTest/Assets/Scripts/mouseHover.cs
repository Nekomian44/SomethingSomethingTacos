using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mouseHover : MonoBehaviour {

	MeshRenderer renderer;
	public bool isStart;
	public bool isExit;
	public AudioClip msover;
	public AudioClip start;
	public AudioClip exit;
	AudioSource aud;

	void Start(){
		renderer = this.GetComponent<MeshRenderer> ();
		aud = this.GetComponent<AudioSource> ();
		renderer.material.color = Color.white;
	}

	void OnMouseEnter(){
		aud.clip = msover;
		aud.Play ();
		renderer.material.color = Color.red;
	}

	void OnMouseExit() {
		renderer.material.color = Color.white;
	}

	void OnMouseUp(){
		if(isStart)
		{
			aud.clip = start;
			aud.Play ();
			// TODO insert scene name as string in the parenthises
			SceneManager.LoadScene("Scene01");
		}
		if (isExit)
		{
			aud.clip = exit;
			aud.Play ();
			Application.Quit();
		}
	}
}
