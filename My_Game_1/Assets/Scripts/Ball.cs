using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    //we want our ball to start with some velocity
    public float ballStartVelocity = 750f;

    public Rigidbody2D ballCollider;
    public bool liveBall;

	public float angle;
	public float ballPos;
	public float posDiff;

	public GameObject PATTLE;
	//protected transform pTransform;

	// Use this for initialization
	void Awake () {
        ballCollider = GetComponent<Rigidbody2D>();
		
	}
	
	// if starting a new ball reset everything, give ball starting velocity
	void Update () {

		if (Input.GetButtonDown ("Fire1") && liveBall == false) {
			transform.parent = null;
			liveBall = true;
			ballCollider.isKinematic = false;
			ballCollider.AddForce (new Vector3 (0, ballStartVelocity, 0));
		}

		 angle = Vector3.Angle(new Vector3(1,0,0), transform.position - new Vector3(0,-0.75f,0) - PATTLE.transform.position);// ballPos = transform.Position.x;

	}

	 void OnCollisionEnter2D(Collision2D other)
		{
			ballCollider.velocity = new Vector2(Mathf.Cos(angle) + ballCollider.velocity.x, ballCollider.velocity.y);	
			print (Mathf.Cos (angle));
		}

}
