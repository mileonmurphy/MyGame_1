using UnityEngine;
using System.Collections;

public class sharkRaise : MonoBehaviour {

	public Portals portalParent;

	public bool hit;

	// Use this for initialization
	void Start () {
		hit = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void MoveUp()
	{
		transform.Translate(Vector3.up * Time.deltaTime * 10);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Ball"))
		{
			hit = true;
			other.gameObject.GetComponent<Ball>().ResetBallNoLife();
		}

		if (other.gameObject.CompareTag ("SharkCol")) {
			hit = true;
		}
	}
}
