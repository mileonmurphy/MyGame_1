using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    //we want our ball to start with some velocity
    public float ballStartVelocity; // set in inspector
	public float maxSpeed; // set in inspector

    public Rigidbody2D ballCollider;
    public bool liveBall, firstHit;

	public float angle;
	public float ballPos;
	public float posDiff;

	public float deflectionPower = 5;
	public float centerWidth = 0.6f;

	protected Transform lastPos;
	public bool headedDown;

	public GameObject livePattle;

	float timeSinceHitPattle;

	gameController gc;
	//protected transform pTransform;

	// Use this for initialization
	void Awake () {
		gc = gameController.instance;
        ballCollider = GetComponent<Rigidbody2D>();
		ballCollider.isKinematic = false;
	}
	
	// if starting a new ball reset everything, give ball starting velocity
	void Update () {
		if (liveBall) timeSinceHitPattle += Time.deltaTime;
		// if stuck or other
		if (timeSinceHitPattle > 4) {
			gc.distractionMananger.CreateWormhole ();
			timeSinceHitPattle = 0;
		}

		if ((Input.GetButtonDown ("Fire1") || Input.GetKey(KeyCode.Space)) && liveBall == false && transform.parent != null) {
			transform.parent = null;
			liveBall = true;
			firstHit = false;
			ballCollider.isKinematic = false;
			ballCollider.AddForce (new Vector3 (0, ballStartVelocity, 0));
		}

		//if ball gets stuck in horizontal motion
		if (ballCollider.velocity.y == 0 && ballCollider.velocity.x != 0) {
			ResetBall();
		}
		// angle = Vector3.Angle(new Vector3(1,0,0), transform.position - new Vector3(0,-0.75f,0) - PATTLE.transform.position);// ballPos = transform.Position.x;
		ballCollider.velocity = Vector3.ClampMagnitude(ballCollider.velocity, maxSpeed);

		// extra balls get destroyed
		if (transform.position.y < -5 && liveBall == false) {
			GameObject.Destroy (gameObject);
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		firstHit = true;
		if (other.gameObject.CompareTag ("Pattle")) {
			/*ballCollider.velocity = new Vector2 ((Mathf.Cos (angle) * -3) + ballCollider.velocity.x, ballCollider.velocity.y);
			if (ballCollider.velocity.magnitude > maxSpeed) {
				ballCollider.velocity = ballCollider.velocity.normalized * maxSpeed;
			}*/
			//print ("pattle");
			float ballX = ballCollider.transform.position.x;
			float pattleX = other.transform.position.x;
			// if on the left side
			if (ballX < pattleX - centerWidth / 2.0f) {
				// deflect left
				ballCollider.velocity = new Vector2 (ballCollider.velocity.x - deflectionPower, ballCollider.velocity.y);
			// if in the center
			} else if (ballX < pattleX + centerWidth / 2.0f) {
				// just bounce normally
				// do nothing
			// if on the right
			} else {
				// deflect right
				ballCollider.velocity = new Vector2 (ballCollider.velocity.x + deflectionPower, ballCollider.velocity.y);
			}
			ballCollider.velocity = ballCollider.velocity.normalized * maxSpeed;
			headedDown = false;
			gc.sounds.Play ("paddle hit");
			timeSinceHitPattle = 0;
		} else if (other.gameObject.name.Contains ("Block")) {
			gc.sounds.Play ("brick hit");
			headedDown = true;
		} else if (other.gameObject.CompareTag ("Portal")) {
			//do nothing
		} else if (other.gameObject.CompareTag ("Guy")) {
			gc.sounds.Play ("hit guy");
		}else{
			gc.sounds.Play("wall hit");
		}
		//print (Mathf.Cos (angle));
	}

	void OnBecameInvisible()
	{
		gameController.instance.loseLife ();
		liveBall = false;
		firstHit = false;
		ballCollider.isKinematic = true;
		if (livePattle) {
			transform.position = livePattle.transform.position;
			transform.position += new Vector3 (0f, .75f, 0f);
			transform.SetParent (livePattle.transform);
		}
	}

	public void ResetBall(){
		headedDown = false;
		gameController.instance.loseLife ();
		liveBall = false;
		firstHit = false;
		ballCollider.velocity = new Vector2 (0, 0);
		ballCollider.isKinematic = true;
		if (livePattle) {
			transform.position = livePattle.transform.position;
			transform.position += new Vector3 (0f, .75f, 0f);
			transform.SetParent (livePattle.transform);
		}
	}

	//find out if ball is headed down
	public bool GetHeadedDown(){
		return headedDown;
	}
}
