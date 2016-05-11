using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slider : MonoBehaviour {

	float scale = 7.0f;
	float mirror = -1.0f;
	float speed = 0.5f;
	public Sprite[] skins; // set in inspector
	public string[] sounds;

	public Vector3 leftEnd;
	public Vector3 rightEnd;

	public enum SliderState { WAITING_ON_LEFT, WAITING_ON_RIGHT, MOVING_LEFT, MOVING_RIGHT }

	protected gameController gc;

	float timer = 0f;

	SliderState state;

	// Use this for initialization
	void Start () {
		state = SliderState.WAITING_ON_LEFT;
		gc = gameController.instance;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		transform.localScale = new Vector3(mirror*scale,scale,scale);
		switch (state) {
		case SliderState.WAITING_ON_LEFT:
			break;
		case SliderState.WAITING_ON_RIGHT:
			break;
		case SliderState.MOVING_LEFT:
			mirror = 1.0f;
			transform.position = Vector3.Lerp (rightEnd, leftEnd, timer * speed);
			if (timer * speed > 1)
				state = SliderState.WAITING_ON_LEFT;
			break;
		case SliderState.MOVING_RIGHT:
			mirror = -1.0f;
			transform.position = Vector3.Lerp (leftEnd, rightEnd, timer * speed);
			if (timer * speed > 1)
				state = SliderState.WAITING_ON_RIGHT;
			break;
		}
	}

	void pickRandomSkin() {
		int idx = Random.Range (0, skins.Length);
		GetComponent<SpriteRenderer> ().sprite = skins [idx];
		gc.sounds.Play (sounds [idx]);
	}

	public void go() {
		if (state == SliderState.WAITING_ON_LEFT) {
			pickRandomSkin ();
			state = SliderState.MOVING_RIGHT;
			timer = 0;
		} else if (state == SliderState.WAITING_ON_RIGHT) {
			pickRandomSkin();
			state = SliderState.MOVING_LEFT;
			timer = 0;
		}
	}
}
