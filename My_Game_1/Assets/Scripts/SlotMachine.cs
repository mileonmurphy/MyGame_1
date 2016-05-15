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

	// Use this for initialization
	void Start () {
		startSlots ();
	}

	int prevState = -1;
	float interval = 0.5f;
	
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
				int result = (int)Mathf.Floor (rollers [rollers.Length - 1].localRotation.eulerAngles.y / 120.0f);
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
				int result = (int)Mathf.Floor (rollers [rollers.Length - 2].localRotation.eulerAngles.y / 120.0f);
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
				results [0] = result;
				rollers [rollers.Length - 3].rotation = Quaternion.Euler (
					120 * result - 9, 0, -90);
				prevState = state;
				// evaluate prize
				giveReward ();
			}

			break;
		// reset
		case 5:
			timer = 0; // reset
			state = 1; // idle
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

	void startSlots() {
		state = 1;
	}

	void randomizeSlots() {
		bool cheating = (Random.value < cheatFrequency);
		// make the game more interesting by having more triples (only works sometimes)
		if (cheating) {
			print ("cheating");
			int r = (Random.Range (0, 360));
			for (int i=0; i<rollers.Length; i++) {
				rollers [i].rotation = Quaternion.Euler (r-interval*degreesPerSecond*i, 0f, -90f);	
			}
		} else {
			for (int i=0; i<rollers.Length; i++) {
				rollers [i].rotation = Quaternion.Euler (Random.Range (0, 360), 0f, -90f);	
			}
		}
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
			GameObject coin = (GameObject)GameObject.Instantiate (coinPrefab, transform.position, Quaternion.identity);
			coin.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-10, 10), Random.Range (-10, 10));
		}
	}

	void createBalls() {
		// create three balls
		for (var i = 0; i < 3; i++) {
			print (transform.position);
			GameObject ball = (GameObject)GameObject.Instantiate (ballPrefab, transform.position + new Vector3(i-1,0,0), Quaternion.identity);
			ball.GetComponent<Rigidbody2D> ().velocity = new Vector2(Random.Range(-5,5),5);
		}
	}

	void setResult(int arg1,int arg2,int arg3) {
		results[0] = arg1;
		results[1] = arg2;
		results[2] = arg3;
	}
}
