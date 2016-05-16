using UnityEngine;
using System.Collections;

public class sharkLower : MonoBehaviour
{

	public Portals portalParent;

	public bool offScreen;

	// Use this for initialization
	void Start()
	{
		offScreen = false;
	}

	// Update is called once per frame
	void Update()
	{
		if(transform.position.y < GameObject.Find("PATTLE").transform.position.y - 3)
		{
			offScreen = true;
		}
	}

	public void MoveDown()
	{
		transform.Translate(-Vector3.up * Time.deltaTime * 10);
	}
}
