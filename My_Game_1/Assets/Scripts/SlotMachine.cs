using UnityEngine;
using System.Collections;

public class SlotMachine : MonoBehaviour {

	public Transform[] rollers; // set in inspector
	public GameObject coinPrefab; // set in inspector
	public GameObject ballPrefab; // set in inspector

	int state;
	float timer = 0;
	int[] results = new int[3];
	float degreesPerSecond = 360;

	float cheatFrequency = 0.5f; // 50% of the results will be triples
	bool cheating = false;
	int cheatResult = -1;

	int prevState = -1;
	float interval = 1.4f;

	// Use this for initialization
	void Start () {
		//startSlots ();
	}
	
	// Update is called once per frame
	void Update () {
		if (state > 0) {
			timer += Time.deltaTime;
			state = (int)(timer/interval + 1.0f);
		} 
		switch (state) {
		// spinning all
		case 1:
			if (prevState != state) {
				// play sound
				print(gameController.instance.sounds);
				gameController.instance.sounds.Play ("slootMachine");
				randomizeSlots ();
				prevState = state;
			}
			for (int i = 0; i < rollers.Length; i++) {
				rotateRoller (rollers [i]);
			}
			break;
		// stop wheel 3
		case 2:
			//print ("3: " + rollers [rollers.Length - 1].localRotation.eulerAngles.y);
			// run once
			if (prevState != state) {
				print (rollers [rollers.Length - 1].localRotation.eulerAngles.y);
				int result = (int)Mathf.Floor (rollers [rollers.Length - 1].localRotation.eulerAngles.y / 120.0f);
				cheatResult = result;
				results [2] = result;
				rollers [rollers.Length - 1].rotation = Quaternion.Euler (
					120*result - 9, 0, -90);
				prevState = state;
			}
			for (int i = 0; i < rollers.Length-1; i++) {
				rotateRoller (rollers [i]);
			}
			break;
		// stop wheel 2
		case 3:
			// run once
			if (prevState != state) {
				print(rollers [rollers.Length - 2].localRotation.eulerAngles.y);
				int result = (int)Mathf.Floor (rollers [rollers.Length - 2].localRotation.eulerAngles.y / 120.0f);
				if (cheating)
					result = cheatResult;
				results [1] = result;
				rollers [rollers.Length - 2].rotation = Quaternion.Euler (
					120*result - 9, 0, -90);
				prevState = state;
			}
			for (int i = 0; i < rollers.Length-2; i++) {
				rotateRoller (rollers [i]);
			}
			break;
		// stop wheel 1 and give prize
		case 4:
			// run once
			if (prevState != state) {
				int result = (int)Mathf.Floor (rollers [rollers.Length - 3].localRotation.eulerAngles.y / 120.0f);
				if (cheating)
					result = cheatResult;
				results [0] = result;
				rollers [rollers.Length - 3].rotation = Quaternion.Euler (
					120 * result - 9, 0, -90);
				prevState = state;
			}

			break;
		// reset
		case 5:
			// evaluate prize
			giveReward ();
			timer = 0; // reset
			state = 0; // stop
			break;
		// idle
		default:
			break;
		}
	}

	float rotationSpeed;
	void rotateRoller(Transform roller) {
		roller.transform.Rotate(0, timer*degreesPerSecond, 0);
	}

	// begins spinning
	public void startSlots() {
		state = 1;
		cheating = (Random.value < cheatFrequency);
	}

	// sets position and begins spinning
	public void startSlots(Vector3 pos) {
		state = 1;
		transform.position = pos + new Vector3(-0.05f,10000,0);
	}

	// runs on start, makes it so that the results aren't always the same
	void randomizeSlots() {
		// make the game more interesting by having more triples (only works sometimes)
		/*if (cheating) {
			print ("cheating");
			int r = (Random.Range (0, 360));
			for (int i=0; i<rollers.Length; i++) {
				rollers [i].rotation = Quaternion.Euler (r - interval*degreesPerSecond*i, 0f, -90f);	
			}
		} else { */
			for (int i=0; i<rollers.Length; i++) {
				rollers [i].rotation = Quaternion.Euler (Random.Range (0, 360), 0f, -90f);	
			}
		//}
	}

	void giveReward() {
		// three of a kind
		if (results [0] == results [1] && results[1] == results [2]) {
			// guy
			if (results[0] == 0) {
				//print ("guy");
				gameController.instance.guy.Sit();
			}
			// coin
			if (results[0] == 1) {
				//print ("coin");
				createCoins();
			}
			// balls
			if (results[0] == 2) {
				//print ("balls");
				createBalls();
			}
		}
	}

	void createCoins() {
		for (var i = 0; i < 20; i++) {
			GameObject coin = (GameObject)GameObject.Instantiate (coinPrefab, transform.position + new Vector3(0,-0.5f,0), Quaternion.identity);
			coin.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-10, 10), Random.Range (-10, 10));
		}
	}

	void createBalls() {
		// create three balls
		for (var i = 0; i < 3; i++) {
			print (transform.position);
			GameObject ball = (GameObject)GameObject.Instantiate (ballPrefab, transform.position + new Vector3(i-1,-0.5f,0), Quaternion.identity);
			ball.GetComponent<Rigidbody2D> ().velocity = new Vector2(Random.Range(-5,5),5);
		}
	}

	void setResult(int arg1,int arg2,int arg3) {
		results[0] = arg1;
		results[1] = arg2;
		results[2] = arg3;
	}

	void OnCollisionExit2D(Collision2D other)
	{
		startSlots ();

	}

	public void EndLevelFast() {
		//createCoins ();
		createBalls ();
	}
}
