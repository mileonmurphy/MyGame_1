using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	/***** SET IN INSPECTOR *****/
	public AudioSource ambient;
	public AudioSource primary;
	public AudioSource secondary;
	public AudioSource tertiary;
	public AudioClip blipSound;
	public AudioClip bloopSound;
	public AudioClip breakSound;
	public AudioClip buildSound;
	public AudioClip changeSound;
	public AudioClip selectSound;
	public AudioClip carSound;
	public AudioClip trainSound;
	public AudioClip planeSound;
	public AudioClip coconutSound;
	public AudioClip hitGuySound;
	public AudioClip meowSound;
	public AudioClip wormhole;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(string soundName) {
		AudioSource source = primary;
		if (primary.isPlaying) {
			source = secondary;
			//print ("second AS");
			if (secondary.isPlaying) {
				source = tertiary;
				//print ("third AS");
			}
		}



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
		case "menu change":
			source.clip = changeSound;
			break;
		case "menu select":
			source.clip = selectSound;
			break;
		case "build":
			source.clip = buildSound;
			break;
		case "car":
			source.clip = carSound;
			break;
		case "train":
			source.clip = trainSound;
			break;
		case "plane":
			source.clip = planeSound;
			break;
		case "coconut":
			source.clip = coconutSound;
			break;
		case "hit guy":
			source.clip = hitGuySound;
			break;
		case "meow":
			source.clip = meowSound;
			break;
		case "wormhole":
			source.clip = wormhole;
			break;
		default:
			break;
		}
		source.Play ();
	}

	public void PlaySong(string title) {
		// ambient.clip = song[title]
		ambient.Play ();
	}
}
