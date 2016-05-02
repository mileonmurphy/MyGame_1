using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class manageDistractions : MonoBehaviour {

	public GameObject kittenHolderObject;
	public GameObject kcl, kcr, kjl, kjr;
	public static GameObject khT, kclT, kcrT, kjlT, kjrT;

	protected enum Distractions { REBUILD_BRICK, WORMHOLE, ROULETTE, KITTENS, SIT_ON_PATTLE }

	protected List<Distractions> distractionQueue;

	public GameObject pattleTarget;

	protected float timer;

	// Use this for initialization
	void Start () {
		pattleTarget = GameObject.Find ("PATTLE");
		distractionQueue = new List<Distractions>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.K)) {
			spawnKittens();
		}
		timer += Time.deltaTime;
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

	public void QueueRandomDistraction() {
		
	}
}
