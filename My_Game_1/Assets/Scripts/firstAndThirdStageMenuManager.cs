using UnityEngine;
using System.Collections;



public class firstAndThirdStageMenuManager : MonoBehaviour {
    enum menuState
    {
        selectAction,
        selectAttack,
        selectTarget,
        playAttack,
        brickDestroyed
    }
    GameObject brickSelector;
	GameObject brickSelector_menu;
	GUIText bounceBallText;
	GameObject bounceBallSelector;
	GameObject attacksListBack;
    GameObject attackBall;
    bool ballBounced;

    private menuState currentState;

    protected gameController gc;

    // Use this for initialization
    void Start () 
	{
        gc = gameController.instance;

		brickSelector = GameObject.Find ("BrickSelector");
		brickSelector_menu = GameObject.Find ("BrickSelector_menu");
		bounceBallText = GameObject.Find ("BounceBallText").GetComponent<GUIText>();
		bounceBallSelector = GameObject.Find ("BounceBallSelector");
		attacksListBack = GameObject.Find ("AttacksListBack");
        attackBall = GameObject.Find("Ball");
        ballBounced = false;

        currentState = menuState.selectAction;
	}
	
	// Update is called once per frame
	void Update () 
	{
        switch(currentState)
        {
            case menuState.selectAction:
                brickSelector.GetComponent<Renderer>().enabled = false;
                brickSelector_menu.GetComponent<Renderer>().enabled = false;
                bounceBallText.enabled = false;
                bounceBallSelector.GetComponent<Renderer>().enabled = false;
                attacksListBack.GetComponent<Renderer>().enabled = false;
                attackBall.GetComponent<Renderer>().enabled = false;

                if (Input.GetKeyDown("return"))
                    currentState = menuState.selectAttack;
                break;

            case menuState.selectAttack:
                brickSelector.GetComponent<Renderer>().enabled = false;
                brickSelector_menu.GetComponent<Renderer>().enabled = false;
                bounceBallText.enabled = true;
                bounceBallSelector.GetComponent<Renderer>().enabled = true;
                attacksListBack.GetComponent<Renderer>().enabled = true;
                attackBall.GetComponent<Renderer>().enabled = false;

                if (Input.GetKeyDown("return"))
                    currentState = menuState.selectTarget;
                break;

            case menuState.selectTarget:
                brickSelector.GetComponent<Renderer>().enabled = true;
                brickSelector_menu.GetComponent<Renderer>().enabled = true;
                bounceBallText.enabled = true;
                bounceBallSelector.GetComponent<Renderer>().enabled = true;
                attacksListBack.GetComponent<Renderer>().enabled = true;
                attackBall.GetComponent<Renderer>().enabled = false;

                if (Input.GetKeyDown("return"))
                    currentState = menuState.playAttack;
                break;

            case menuState.playAttack:
                brickSelector.GetComponent<Renderer>().enabled = false;
                brickSelector_menu.GetComponent<Renderer>().enabled = false;
                bounceBallText.enabled = false;
                bounceBallSelector.GetComponent<Renderer>().enabled = false;
                attacksListBack.GetComponent<Renderer>().enabled = false;
                attackBall.GetComponent<Renderer>().enabled = true;

                if(!ballBounced)
                {
                    attackBall.transform.position = Vector3.MoveTowards(attackBall.transform.position, GameObject.Find("Paddle").transform.position, Time.deltaTime * 10);
                    if (attackBall.transform.position == GameObject.Find("Paddle").transform.position)
                        ballBounced = true;
                }
                else
                {
                    attackBall.transform.position = Vector3.MoveTowards(attackBall.transform.position, GameObject.Find("Brick").transform.position, Time.deltaTime * 10);
                    if (attackBall.transform.position == GameObject.Find("Brick").transform.position)
                        currentState = menuState.brickDestroyed;
                }
                break;

            case menuState.brickDestroyed:
                brickSelector.GetComponent<Renderer>().enabled = false;
                brickSelector_menu.GetComponent<Renderer>().enabled = false;
                bounceBallText.enabled = false;
                bounceBallSelector.GetComponent<Renderer>().enabled = false;
                attacksListBack.GetComponent<Renderer>().enabled = false;
                attackBall.GetComponent<Renderer>().enabled = false;
                GameObject.Find("Brick").GetComponent<Renderer>().enabled = false;
                Application.LoadLevel("Game");
                break;
        }
	}
}
