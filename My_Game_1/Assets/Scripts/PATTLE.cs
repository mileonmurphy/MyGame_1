using UnityEngine;
using System.Collections;

public class PATTLE : MonoBehaviour {


	public Vector3 pattlePosition;
	public static Vector3 pattleVelocity;
	public static float pattleSpeed = 0.2f;

	public bool touchBall;
	public bool hazKittens;


	bool leftPressed;
	bool rightPressed;


	// start our paddle in the designated position, assign pattlePosition
	void Start () {
		//pattlePosition = transform.position;
	}
	
	// move the paddle based on player input, update vars
	void Update () {



		if (Input.GetKeyDown (KeyCode.LeftArrow))
			leftPressed = true;

		if (Input.GetKeyDown (KeyCode.RightArrow))
			rightPressed = true;

		if (Input.GetKeyUp (KeyCode.LeftArrow))
			leftPressed = false;

		if (Input.GetKeyUp (KeyCode.RightArrow))
			rightPressed = false;

		int dir = 0;
		if (leftPressed) dir -= 1;
		if (rightPressed)dir += 1;

        float xPos = transform.position.x + (dir * pattleSpeed);

		print (dir);

        //update paddle position, Clamp lets us create boundaries for movement
        pattlePosition = new Vector3(Mathf.Clamp(xPos, -7.25f, 7.25f), -4.49f, -1f);
        transform.position = pattlePosition;



    }

	public static Vector2 getVel(){
		return new Vector2(pattleVelocity.x, pattleVelocity.y);
	}
}
