using UnityEngine;
using System.Collections;

public class Portals : MonoBehaviour {

	public GameObject ballRef;
	public GameObject pattleRef;
	protected Rigidbody2D ballCollider;

	protected int state = 1;
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
		//reference to portal object
		GameObject port = null;

		//if ball is headed down and somewhat in middle of screen
		if (ballRef.GetComponent<Ball> ().GetHeadedDown()){ //&& (ballRef.transform.position - pattleRef.transform.position).magnitude > 2){
			targetPos = new Vector3(ballCollider.position.x + (ballCollider.velocity.normalized * targDist).x, ballCollider.position.y + (ballCollider.velocity.normalized * targDist).y, 0);
			//instantiate portal where ball is headed - start small and have it grow
			Instantiate(portalPrefab, targetPos, Quaternion.Euler(0,0,0));
			port = GameObject.FindGameObjectsWithTag("Portal")[0];
			minScale = port.transform.localScale.magnitude;
			portalExists = true;
		}

		for (int i = 0; i < GameObject.FindGameObjectsWithTag("Portal").Length; i++) {
			GameObject temp = GameObject.FindGameObjectsWithTag("Portal")[i];
			if(i >= 1){
				Destroy(temp);
			}
		}

		if (portalExists) {
			//scale portal up
			if(port != null){
				port.transform.localScale += Vector3.one * scaleRate;

				//spin portal
				port.transform.Rotate(Vector3.back, 2.0f);
			}
		}
	}

	protected void MakingShark(){

	}

	protected void MakingFrog(){

	}

	public void EndPortal(){
		portalExists = false;
	}
}
