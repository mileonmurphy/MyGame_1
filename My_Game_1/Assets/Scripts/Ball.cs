using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    //we want our ball to start with some velocity
    public float ballStartVelocity = 200f;

	float maxSpeed = 200f;

    public Rigidbody2D ballCollider;
    public bool liveBall;

	public float angle;
	public float ballPos;
	public float posDiff;

	public PATTLE pattle;
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
		Debug.DrawLine (transform.position, pattle.transform.position,Color.red);
		angle = Vector3.Angle(new Vector3(1,0,0), transform.position - new Vector3(0,-0.75f,0) - pattle.transform.position);// ballPos = transform.Position.x;

	}

	 void OnCollisionEnter2D(Collision2D other)
		{
		if (other.gameObject.CompareTag ("Pattle")) {
			ballCollider.velocity = new Vector2 ((Mathf.Cos (angle) * -3) + ballCollider.velocity.x, ballCollider.velocity.y);
			//ballCollider.velocity += pattle.getVel();
			if(ballCollider.velocity.magnitude >= maxSpeed){
				ballCollider.velocity = ballCollider.velocity.normalized * maxSpeed;
			}
		}
			print (Mathf.Cos (angle));
		}

}
