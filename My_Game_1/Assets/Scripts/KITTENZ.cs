using UnityEngine;
using System.Collections;

public class KITTENZ : MonoBehaviour {

	public GameObject kittenCrouchL;
	public GameObject kittenCrouchR;
	public GameObject kittenJumpL;
	public GameObject kittenJumpR;

	public float kittenForce; // set in inspector
	public float kittenAngle; // set in inspector

	public float delayTime; // set in inspector

	public static KITTENZ instance = null;


	void Awake()
	{
		instance = this;
	}

	public void getRefs()
	{
		kittenCrouchL = manageDistractions.kclT;
		kittenCrouchR = manageDistractions.kcrT;
		kittenJumpL = manageDistractions.kjlT;
		kittenJumpR = manageDistractions.kjrT;
	}
	// Use this for initialization
	public void hideKitties () {
		kittenJumpL.GetComponent<SpriteRenderer> ().enabled = false;
		kittenJumpR.GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		//if we want we can have individual sides activate based on where the ball is 
		//when ball enters trigger box, switch sprites, apply kittenforce, destroy kittens after a short delay
		if (other.gameObject.CompareTag ("Ball") && other.gameObject.GetComponent<Ball>().firstHit == true) {
			kittenCrouchL.GetComponent<SpriteRenderer> ().enabled = false;
			kittenCrouchR.GetComponent<SpriteRenderer> ().enabled = false;
			kittenJumpL.GetComponent<SpriteRenderer> ().enabled = true;
			kittenJumpR.GetComponent<SpriteRenderer> ().enabled = true;

			kittenAngle =  Mathf.Cos (Random.Range(-.75f, .75f));
			other.GetComponent<Rigidbody2D>().velocity = new Vector2 ((kittenAngle * -3) + other.GetComponent<Rigidbody2D>().velocity.x, other.GetComponent<Rigidbody2D>().velocity.y + kittenForce);

			Invoke("removeKittens", delayTime);
			//print (gameController.instance.sounds);
			gameController.instance.sounds.Play ("meow");

		}
	}

void removeKittens()
	{
		Destroy (gameObject);
	}
}
