using UnityEngine;
using System.Collections;

public class Wormhole : MonoBehaviour {

	public Portals portalParent;

	public float scaleRate = 0.01f;
	public float maxScale = 5.0f;
	public float minScale = 0.0f;

	bool isShrinking = false;

	// Use this for initialization
	void Start () {
		//print (gameObject.GetComponent<Collider2D> ().isTrigger);
	}

	// Update is called once per frame
	void Update () {
		MakingPortal ();
	}

	protected void MakingPortal(){

		if (true) {
			//scale portal up
				if (isShrinking)
				{
					Shrinking();
				}
				else
				{
					Growing();
				}

				//spin portal
				transform.Rotate(Vector3.back*Time.deltaTime*60f, 2.0f);
		}
	}

	protected void MakingShark(){

	}

	protected void MakingFrog(){

	}

	public void Shrinking()
	{
		transform.localScale -= Vector3.one * scaleRate * Time.deltaTime * 60f;
		//print ("" + transform.localScale.x + " < " + minScale);
		if (transform.localScale.x < minScale) {
			DestroyPortal ();
		}
	}

	public void Growing()
	{
		transform.localScale += Vector3.one * scaleRate * Time.deltaTime * 60f;
		if (transform.localScale.x > maxScale) {
			isShrinking = true;
		}
	}

	//destroy portal
	public void DestroyPortal()
	{
		portalParent.EndPortal ();
		Destroy(GameObject.FindGameObjectWithTag("Portal"));
	}

	void OnTriggerEnter2D(Collider2D other){
		//print ("tigger");
		if (other.gameObject.CompareTag ("Ball")) {
			//FlipScaleRate();
			//Invoke("DestroyPortal", 0.5f);
			// flip
			isShrinking = true;
			gameController.instance.lives++;
			other.gameObject.GetComponent<Ball>().ResetBall();
			gameController.instance.sounds.Play ("wormhole");
		}
	}

}
