using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AnnoyingGuy : MonoBehaviour {

	protected int state = 0;
	protected float actionTimer = 0;
	protected float animTimer = 0;
	protected float hammerTimer = 0;
	protected Vector3 targetPos;
	protected Vector3 startPos;
	public brick brickPrefab;
	protected GameObject brickToBuild;
	float targetDist;
	float speed = 5;
	protected SpriteRenderer sr;
	public Sprite[] pose;
	public Sprite[] build;
	public Sprite hitUp;
	public Sprite hitDown;
	public Sprite sit;
	protected Vector3 hammerOffset;
	protected gameController gc;

	protected List<GameObject> brickQueue;

	// Use this for initialization
	void Start () {
		gc = gameController.instance;
		sr = GetComponent<SpriteRenderer> ();
		brickQueue = new List<GameObject> ();
	}

	public void SetState(int s) {
		state = s;
	}

	// Update is called once per frame
	void Update () {
		actionTimer += Time.deltaTime;
		hammerTimer += Time.deltaTime;
		animTimer += Time.deltaTime;
		// do stuff based on state
		switch (state) {
		case 0:
			break;	
		case 1:
			MovingToBrick ();
			AnimateWalk();
			break;
		case 2:
			BuildingBrick ();
			AnimateBuild ();
			break;
		case 3:
			MoveOffscreen ();
			AnimateWalk ();
			break;
		case 4:
			Sitting ();
			break;
		default:
			break;
		}
	}

	public void rebuildBrick(GameObject brickToBuildArg) {
		state = 1;
		brickToBuild = brickToBuildArg;
		brickQueue.Add (brickToBuildArg);
		targetPos = brickToBuild.transform.position + new Vector3 (-1, 10000 - 1.5f, -1f);
		startPos = transform.position;
		targetDist = (targetPos - startPos).magnitude;
		actionTimer = 0;
	}

	protected void MovingToBrick() {
		// move towards brick
		transform.position = Vector3.Lerp(startPos, targetPos, speed * actionTimer / targetDist);
		// if you reach the brick, change state
		if (speed * actionTimer > targetDist) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, -2);
			state = 2;
			actionTimer = 0;
		}
	}

	protected void BuildingBrick() {
		// sound
		if (hammerTimer > 0.8) {
			hammerTimer = 0;
			gc.sounds.Play ("build");
		}

		// timer until complete
		if (actionTimer > 1.9) {
			state = 0;
			// move brick back in front of the camera
			brickToBuild.transform.position = new Vector3(
				brickToBuild.transform.position.x,
				brickToBuild.transform.position.y+10000,
				-1);
            gc.bricks++;
            //GameObject.Instantiate (brickPrefab,targetPos,Quaternion.identity);
            brickQueue.RemoveAt (brickQueue.Count-1); // pop last brick
			if (brickQueue.Count > 0)
				rebuildBrick (brickQueue [brickQueue.Count - 1]);
			else {
				state = 3; // move offscreen
				actionTimer = 0;
				startPos = transform.position;
			}
			
		}
	}

	// play the walk animation
	protected void AnimateWalk() {
		// every 1 seconds
		if (animTimer > 1f) {
			// max length
			animTimer -= 1f;

			// set sprite to next in sequence
			sr.sprite = pose [(int)Mathf.Floor (Random.Range (0, pose.Length))];
		}
	}

	protected void AnimateBuild() {
		int speed = 2;

		// max length
		while (animTimer > build.Length/speed) {
			animTimer -= build.Length/speed;
		}
		// set sprite to next in sequence
		sr.sprite = build [(int)Mathf.Floor (animTimer*speed)];
	}

	protected void MoveOffscreen() {
		// move towards top
		transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x,200,-1), speed * actionTimer / 100f);
		// if you are done, change state
		if (speed * actionTimer / 100f > targetDist) {
			state = 0;
		}
	}

	public void Sit () {
		state = 4;
	}

	protected void Sitting() {
		sr.sprite = sit;
		transform.position = gc.pattle.transform.position + new Vector3(0,2.2f,-2);
		gc.pattle.GetComponent<PATTLE>().SlowPattle ();
	}

	// when hit
	void OnCollisionEnter2D(Collision2D col) {
		// hit with ball
		if (col.gameObject.CompareTag ("Ball") && state != 3) {
			SetState (3); // run away
			actionTimer = 0;
			startPos = transform.position;
			gc.Rebuild = false;
		}
	}
}
