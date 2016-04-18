using UnityEngine;
using System.Collections;

public class PATTLE : MonoBehaviour {


	public Vector3 pattlePosition;
	public Vector3 pattleVelocity;
	public float pattleSpeed = 10f;

	public bool touchBall;
	public bool hazKittens;



	// Use this for initialization
	void Start () {
		pattlePosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(Input.GetAxis("Horizontal") * pattleSpeed, pattlePosition.y, -1) ;



	}
}
