﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

    public int lives = 3;
    public int bricks = 20;
    public float resetDelay = 1f;
    public bool gameOver;
    public bool won, lost;
	public static int score, bricksDestroyed = 0;
	public Vector3 ballPos;

    public GameObject brickFormation;
    public GameObject pattle;
	public GameObject ball;
	public GameObject clonePattle;

	public GameObject scoreTextObj, lifeTextObj;
	public Text scoreText, lifeText;
	public AnnoyingGuy Guy;

    public static gameController instance = null;
    //public GameObject clonePattle;

	// Use this for initialization
	void Awake () {

		//we use instance to reference methods in other scripts
		//this code prevents more than one game controller being made
		//used for if we have players looping through menus
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Setup();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Setup()
    {
		//clonePattle = Instantiate (pattle, new Vector3(-.146f,-4.49f,-1f), Quaternion.identity) as GameObject;
		//Instantiate (brickFormation, new Vector3 (1.2612f, .1567f, -1f), Quaternion.identity);
    }

    void gameStatus()
    {
        if(bricks < 1)
        {
            won = true;
            Time.timeScale = .25f;
			//instantiate new brick formation
            //Invoke("Reset", resetDelay);
        }

        if(lives < 1)
        {
            gameOver = true;
            lost = true;

            //might change this to load YOU DIED screen
			Application.LoadLevel("EndGame");
        }
    }
	

    public void loseLife()
    {
        lives--;
		lifeText = lifeTextObj.GetComponent<Text> ();
		lifeText.text = "Lives: " + lives.ToString ();
		//Destroy (clonePattle);
        //Invoke("SetUpPattle", resetDelay);
        gameStatus();
    }

    void SetUpPattle()
    {
		//clonePattle = Instantiate (pattle, new Vector3(-.146f,-4.49f,-1f), Quaternion.identity) as GameObject;
		//ballPos = pattle.transform.position;
		//ballPos.y += .1f;
		//Instantiate (ball, ballPos, Quaternion.identity);
		//ball.transform.parent = pattle.transform;

    }

    public void DestroyBrick()
    {
        bricks--;
		bricksDestroyed++;
		score += 30 * bricksDestroyed;
		scoreText = scoreTextObj.GetComponent<Text> ();
		scoreText.text = score.ToString ();
        gameStatus();
    }
}
