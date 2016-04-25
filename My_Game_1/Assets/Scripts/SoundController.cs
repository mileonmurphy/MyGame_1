using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public AudioSource source;
	public AudioClip blipSound;
	public AudioClip bloopSound;
	public AudioClip breakSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(string soundName) {
		switch (soundName) {
		case "wall hit":
			source.clip = bloopSound;
			break;
		case "paddle hit":
			source.clip = blipSound;
			break;
		case "brick hit":
			source.clip = breakSound;
			break;
		default:
			break;
		}
		source.Play ();
	}
}
