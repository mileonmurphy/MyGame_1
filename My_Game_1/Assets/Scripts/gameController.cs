using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

    public int lives = 3;
    public int bricks = 20;
    public float resetDelay = 1f;
    public bool gameOver;
    public bool won, lost;

    public GameObject brickObject;
    public GameObject pattle;

    public static gameController instance = null;
    public GameObject clonePattle;

	// Use this for initialization
	void Awake () {

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
        clonePattle = Instantiate(pattle, transform.position, Quaternion.identity) as GameObject;
        Instantiate(brickObject, transform.position, Quaternion.identity);
    }

    void gameStatus()
    {
        if(bricks < 1)
        {
            won = true;
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }

        if(lives < 1)
        {
            gameOver = true;
            lost = true;

            //might change this to load YOU DIED screen
            Invoke("Reset", resetDelay);
        }
    }

    void Reset()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("Game");
    }

    public void loseLife()
    {
        lives--;
        //change text of lives UI object
        Destroy(clonePattle);
        Invoke("SetUpPattle", resetDelay);
        gameStatus();
    }

    void SetUpPattle()
    {
        clonePattle = Instantiate(pattle, transform.position, Quaternion.identity) as GameObject;

    }

    public void DestroyBrick()
    {
        bricks--;
        gameStatus();
    }
}
