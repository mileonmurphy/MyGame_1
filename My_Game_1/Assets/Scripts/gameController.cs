using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

    public int lives; // set in inspector
	public int bricks = 20; // set in inspector
	public float resetDelay = 1f; // set in inspector
	public bool gameOver; // set in inspector
	public bool won, lost; // set in inspector
	public static int score, bricksDestroyed = 0; // set in inspector
	public Vector3 ballPos; // set in inspector

	public GameObject brickFormation; // set in inspector
	public GameObject pattle; // set in inspector
	public GameObject ball; // set in inspector
	public GameObject clonePattle; // set in inspector
	public AnnoyingGuy guy; // set in inspector

	public GameObject scoreTextObj, lifeTextObj; // set in inspector
	public Text scoreText, lifeText; // set in inspector

	public SoundController sounds; // set in inspector

	public static gameController instance = null; // NOT set in inspector (singleton)
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
		sounds = GetComponent<SoundController>();
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
		if (lifeText) {
			lifeText = lifeTextObj.GetComponent<Text> ();
			lifeText.text = "Lives: " + lives.ToString ();
		}
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

    public void DestroyBrick(GameObject destroyedBrick)
    {
		Vector2 p = destroyedBrick.transform.position; // shorthand
		destroyedBrick.transform.position = new Vector3 (p.x, p.y-10000,-1); // move out of sight
		guy.rebuildBrick(destroyedBrick);
        bricks--;
		bricksDestroyed++;
		score += 30 * bricksDestroyed;
		scoreText = scoreTextObj.GetComponent<Text> ();
		scoreText.text = score.ToString ();
        gameStatus();
    }
}
