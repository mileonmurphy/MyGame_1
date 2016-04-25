using UnityEngine;
using System.Collections;

public class AnnoyingGuy : MonoBehaviour {

	protected int state = 0;
	protected float timer;
	protected Vector3 targetPos;
	protected Vector3 startPos;
	public brick brickPrefab;
	protected GameObject brickToBuild;
	float targetDist;
	float speed = 5;

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

	public void rebuildBrick(GameObject brickToBuildArg) {
		state = 1;
		brickToBuild = brickToBuildArg;
		targetPos = brickToBuild.transform.position;
		startPos = transform.position;
		targetDist = (targetPos - startPos).magnitude;
		timer = 0;
	}

	protected void MovingToBrick() {
		// move towards brick
		transform.position = Vector3.Lerp(startPos, targetPos, speed * timer / targetDist);
		// if you reach the brick, change state
		if (speed * timer > targetDist) {
			state = 2;
			timer = 0;
		}
	}

	protected void BuildingBrick() {
		// timer until complete
		if (timer > 2) {
			state = 0;
			// move brick back in front of the camera
			brickToBuild.transform.position = new Vector3(
				brickToBuild.transform.position.x,
				brickToBuild.transform.position.y+10000,
				-1);
			//GameObject.Instantiate (brickPrefab,targetPos,Quaternion.identity);
		}
	}
}
