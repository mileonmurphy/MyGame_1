using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    //we want our ball to start with some velocity
    public float ballStartVelocity = 500f;

    public Rigidbody2D ballCollider;
    public bool liveBall;

	// Use this for initialization
	void Awake () {
        ballCollider = GetComponent<Rigidbody2D>();
	}
	
	// if starting a new ball reset everything, give ball starting velocity
	void Update () {

        if (Input.GetButtonDown("Fire1") && liveBall == false)
        {
            transform.parent = null;
            liveBall = true;
            ballCollider.isKinematic = false;
            ballCollider.AddForce(new Vector3(ballStartVelocity, ballStartVelocity, 0));
        }
    }
}
