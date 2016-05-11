using UnityEngine;
using System.Collections;

public class Portals : MonoBehaviour {

	public GameObject ballRef;
	public GameObject pattleRef;
	protected Rigidbody2D ballCollider;

	protected int state = 0;
    int targDist = 3;
	protected Vector3 targetPos;
	protected float speed = 5.0f;
	protected float timer;
	public bool portalExists;
	float scaleRate = 0.01f;
	float maxScale = 5.0f;
	float minScale;

	public GameObject portalPrefab;
	public GameObject[] sharkPrefabs;
	public GameObject finPrefab;
	public GameObject frogPrefab;

	protected Wormhole wormhole;

    bool isShrinking = false;

    //reference to portal object
    GameObject port = null;

    // Use this for initialization
    void Start () {
		ballCollider = ballRef.GetComponent<Rigidbody2D> ();
		portalExists = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		// do stuff based on state
		switch (state) {
		case 0:
			break;	
		case 1:
			MakingPortal ();
			break;
		case 2:
			MakingShark ();
			break;
		case 3: 
			MakingFrog();
			break;
		default:
			break;
		}
	}

	protected void MakingPortal(){
  		
		//if ball is headed down and somewhat in middle of screen
		if (!portalExists && ballRef.GetComponent<Ball> ().GetHeadedDown()){ //&& (ballRef.transform.position - pattleRef.transform.position).magnitude > 2){

			// put portal ahead of the ball
			targetPos = new Vector3(ballCollider.position.x + (ballCollider.velocity.normalized * targDist).x, ballCollider.position.y + (ballCollider.velocity.normalized * targDist).y, 0);

			if (!GameObject.FindGameObjectWithTag ("PortalCol").GetComponent <BoxCollider2D> ().bounds.Contains (targetPos))
				return;
			//instantiate portal where ball is headed - start small and have it grow
			port = (GameObject)Instantiate(portalPrefab, targetPos, Quaternion.Euler(0,0,0));

			port.GetComponent<Wormhole> ().portalParent = gameObject.GetComponent<Portals>();

			// acknowledge portal
			portalExists = true;

			// stop making new portals
			state = 0;
		}
	}

	protected void MakingShark(){

	}

	protected void MakingFrog(){

	}

	public void EndPortal(){
		portalExists = false;
		port = null;
	}

	public void MakeWormhole() {
		state = 1;
	}
}
