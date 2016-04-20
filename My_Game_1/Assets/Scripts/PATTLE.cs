﻿using UnityEngine;
using System.Collections;

public class PATTLE : MonoBehaviour {


	public Vector3 pattlePosition;
	public static Vector3 pattleVelocity;
	public static float pattleSpeed = 0.1f;

	public bool touchBall;
	public bool hazKittens;



	// start our paddle in the designated position, assign pattlePosition
	void Start () {
		//pattlePosition = transform.position;
	}
	
	// move the paddle based on player input, update vars
	void Update () {

        float xPos = transform.position.x + (Input.GetAxis("Horizontal") * pattleSpeed);

        //update paddle position, Clamp lets us create boundaries for movement
        pattlePosition = new Vector3(Mathf.Clamp(xPos, -8.25f, 8.25f), -4.49f, -1f);
        transform.position = pattlePosition;



    }
}
