using UnityEngine;
using System.Collections;

public class AnnoyingGuy : MonoBehaviour {

	protected int state = 0;
	protected float timer;
	protected Vector3 targetPos;
	protected Vector3 startPos;
	public brick brickPrefab;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		// do stuff based on state
		switch (state) {
		case 0:
			break;	
		case 1:
			MovingToBrick ();
			break;
		case 2:
			BuildingBrick ();
			break;
		default:
			break;
		}
	}

	public void rebuildBrick(Vector3 pos) {
		state = 1;
		targetPos = pos;
		startPos = transform.position;
		timer = 0;
	}

	protected void MovingToBrick() {
		// move towards brick
		transform.position = Vector3.Lerp(startPos, targetPos, timer);
		// if you reach the brick, change state
		if (timer > 1) {
			state = 2;
			timer = 0;
		}
	}

	protected void BuildingBrick() {
		// timer until complete
		if (timer > 1) {
			state = 0;
			GameObject.Instantiate (brickPrefab,targetPos,Quaternion.identity);
		}
	}
}
