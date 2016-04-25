using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    //we want our ball to start with some velocity
    public float ballStartVelocity = 200f;
	public float maxSpeed = 200f;

    public Rigidbody2D ballCollider;
    public static bool liveBall, firstHit;

	public float angle;
	public float ballPos;
	public float posDiff;

	public GameObject livePattle;
	//protected transform pTransform;

	// Use this for initialization
	void Awake () {
        ballCollider = GetComponent<Rigidbody2D>();
	}
	
	// if starting a new ball reset everything, give ball starting velocity
	void Update () {

		//code to fire the ball using either spacebar or mouse click
		if ((Input.GetButtonDown ("Fire1") || Input.GetKey(KeyCode.Space)) && liveBall == false) {
			transform.parent = null;
			liveBall = true;
			firstHit = false;
			ballCollider.isKinematic = false;
			ballCollider.AddForce (new Vector3 (0, ballStartVelocity, 0));
		}

		//needs work
		 angle = Vector3.Angle(new Vector3(1,0,0), transform.position - new Vector3(0,-0.75f,0) - livePattle.transform.position);// ballPos = transform.Position.x;

	}

	 void OnCollisionEnter2D(Collision2D other)
		{
		firstHit = true;
		//sees if it is hitting the pattle, applies directional force
		if (other.gameObject.CompareTag ("Pattle")) {
			ballCollider.velocity = new Vector2 ((Mathf.Cos (angle) * -3) + ballCollider.velocity.x, ballCollider.velocity.y);
		}
		if (ballCollider.velocity.magnitude > maxSpeed) {
			ballCollider.velocity = ballCollider.velocity.normalized * maxSpeed;
		}
		}

	//reposition the ball and reset it when it goes outside the camera view
	void OnBecameInvisible()
	{
		gameController.instance.loseLife ();
		liveBall = false;
		firstHit = false;
		ballCollider.isKinematic = true;
		transform.position = livePattle.transform.position;
		transform.position += new Vector3(0f,.5f,0f);
		transform.SetParent (livePattle.transform);
	}

}
