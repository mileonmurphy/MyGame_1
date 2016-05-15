using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class manageDistractions : MonoBehaviour {

	public GameObject kittenHolderObject;
	public GameObject kcl, kcr, kjl, kjr;
	public static GameObject khT, kclT, kcrT, kjlT, kjrT;

	public enum Distractions { REBUILD_BRICK, SLIDER, SLOT_MACHINE, KITTENS, SIT_ON_PATTLE }
	protected int numDistractions = 5; // the length of the above enum

	protected List<Distractions> distractionQueue;

	public GameObject pattleTarget;

	public Portals portals;

	protected gameController gc;

	public Slider slider;

	protected float timer;
	public float randomDistractionInterval; // set in inspector

	// Use this for initialization
	void Start () {
		pattleTarget = GameObject.Find ("PATTLE");
		distractionQueue = new List<Distractions>();
		gc = GameObject.FindObjectOfType<gameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.K)) {
			spawnKittens();
		}

		// increment timer
		timer += Time.deltaTime;

		// when it is time for another distraction
		if (timer > randomDistractionInterval) {
			// add to queue
			QueueRandomDistraction ();
			timer -= randomDistractionInterval;
		}
	}

	// when a brick is hit, use the next queued distraction
	public void DequeueDistraction() {
		
		// check if there are any distractions
		if (distractionQueue.Count < 1) return;

		// activate
		activateDistraction (distractionQueue[distractionQueue.Count-1]);

		// pop
		distractionQueue.RemoveAt(distractionQueue.Count-1);
	}

	protected void activateDistraction(Distractions d) {
		switch (d) {
		case Distractions.KITTENS:
			spawnKittens ();
			break;
		case Distractions.REBUILD_BRICK:
			gc.Rebuild = true;
			break;
		case Distractions.SLOT_MACHINE:
			gc.Slots = true;
			break;
		case Distractions.SIT_ON_PATTLE:
			gc.guy.Sit ();
			break;
		case Distractions.SLIDER:
			slider.go ();
			break;
		}
	}

	public void spawnKittens()
	{

		khT = Instantiate (kittenHolderObject, new Vector2(pattleTarget.transform.position.x, pattleTarget.transform.position.y + .96f), Quaternion.identity) as GameObject;
		khT.transform.parent = pattleTarget.transform;
		kclT = Instantiate (kcl, new Vector2(pattleTarget.transform.position.x - 1.28f, pattleTarget.transform.position.y + .59f), Quaternion.identity) as GameObject;
		kclT.transform.parent = khT.transform;
		kcrT = Instantiate (kcr, new Vector2(pattleTarget.transform.position.x + 1.22f , pattleTarget.transform.position.y + .59f), Quaternion.identity) as GameObject;
		kcrT.transform.parent = khT.transform;
		kjlT = Instantiate (kjl, new Vector2(pattleTarget.transform.position.x -1.2f , -3.6f), Quaternion.identity) as GameObject;
		kjlT.transform.parent = khT.transform;
		kjrT = Instantiate (kjr, new Vector2(pattleTarget.transform.position.x + 1.2f , -3.6f), Quaternion.identity) as GameObject;
		kjrT.transform.parent = khT.transform;

		KITTENZ.instance.getRefs ();
		KITTENZ.instance.hideKitties ();
	}

	protected void QueueRandomDistraction() {
		//QueueDistraction( (Distractions)Random.Range (0, 4) );
		QueueDistraction((Distractions)2);
		print ("Next up: " + distractionQueue [distractionQueue.Count - 1]);
	}

	public void QueueDistraction(Distractions d) {
		distractionQueue.Add (d);
	}

	public void CreateWormhole() {
		portals.MakeWormhole ();
	}
}
