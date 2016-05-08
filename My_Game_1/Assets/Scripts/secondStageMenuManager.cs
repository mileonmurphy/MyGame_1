using UnityEngine;
using System.Collections;

public class secondStageMenuManager : MonoBehaviour {

    enum menuState
    {
        selectAction,
        selectFireAttack,
        selectFrostAttack,
        selectThunderAttack,
        selectCycloneAttack,
        selectTarget,
        playFireAttack,
        playFrostAttack,
        playThunderAttack,
        playCycloneAttack,
        brickDestroyed
    }
    GameObject brickSelector;
    GameObject brickSelector_menu;
    GUIText    fireBallText;
    GUIText    frostBallText;
    GUIText    thunderBallText;
    GUIText    cycloneBallText;
    GameObject fireBallSelector;
    GameObject frostBallSelector;
    GameObject thunderBallSelector;
    GameObject cycloneBallSelector;
    GameObject attacksListBack;
    GameObject fireBall;
    GameObject frostBall;
    GameObject thunderBall;
    GameObject cycloneBall;
    bool ballBounced;

    private menuState currentState;

    protected gameController gc;

    // Use this for initialization
    void Start ()
    {
        gc = gameController.instance;

        brickSelector = GameObject.Find("BrickSelector");
        brickSelector_menu = GameObject.Find("BrickSelector_menu");
        fireBallText = GameObject.Find("FireBallText").GetComponent<GUIText>();
        frostBallText = GameObject.Find("FrostBallText").GetComponent<GUIText>();
        thunderBallText = GameObject.Find("ThunderBallText").GetComponent<GUIText>();
        cycloneBallText = GameObject.Find("CycloneBallText").GetComponent<GUIText>();
        fireBallSelector = GameObject.Find("FireBallSelector");
        frostBallSelector = GameObject.Find("FrostBallSelector");
        thunderBallSelector = GameObject.Find("ThunderBallSelector");
        cycloneBallSelector = GameObject.Find("CycloneBallSelector");
        attacksListBack = GameObject.Find("AttacksListBack");
        fireBall = GameObject.Find("FireBall");
        frostBall = GameObject.Find("FrostBall");
        thunderBall = GameObject.Find("ThunderBall");
        cycloneBall = GameObject.Find("CycloneBall");
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
                fireBallText.enabled = false;
                frostBallText.enabled = false;
                thunderBallText.enabled = false;
                cycloneBallText.enabled = false;
                fireBallSelector.GetComponent<Renderer>().enabled = false;
                frostBallSelector.GetComponent<Renderer>().enabled = false;
                thunderBallSelector.GetComponent<Renderer>().enabled = false;
                cycloneBallSelector.GetComponent<Renderer>().enabled = false;
                attacksListBack.GetComponent<Renderer>().enabled = false;
                fireBall.GetComponent<Renderer>().enabled = false;
                frostBall.GetComponent<Renderer>().enabled = false;
                thunderBall.GetComponent<Renderer>().enabled = false;
                cycloneBall.GetComponent<Renderer>().enabled = false;

                if (Input.GetKeyDown("return"))
                    currentState = menuState.selectFireAttack;
                break;

            case menuState.selectFireAttack:
                fireBallText.enabled = true;
                frostBallText.enabled = true;
                thunderBallText.enabled = true;
                cycloneBallText.enabled = true;
                fireBallSelector.GetComponent<Renderer>().enabled = true;
                frostBallSelector.GetComponent<Renderer>().enabled = false;
                thunderBallSelector.GetComponent<Renderer>().enabled = false;
                cycloneBallSelector.GetComponent<Renderer>().enabled = false;
                attacksListBack.GetComponent<Renderer>().enabled = true;

                if (Input.GetKeyDown("return"))
                    currentState = menuState.selectTarget;
                if (Input.GetKeyDown("up"))
                    currentState = menuState.selectCycloneAttack;
                if (Input.GetKeyDown("down"))
                    currentState = menuState.selectFrostAttack;
                break;

            case menuState.selectFrostAttack:
                fireBallSelector.GetComponent<Renderer>().enabled = false;
                frostBallSelector.GetComponent<Renderer>().enabled = true;
                thunderBallSelector.GetComponent<Renderer>().enabled = false;
                cycloneBallSelector.GetComponent<Renderer>().enabled = false;
 
                if (Input.GetKeyDown("return"))
                    currentState = menuState.selectTarget;
                if (Input.GetKeyDown("up"))
                    currentState = menuState.selectFireAttack;
                if (Input.GetKeyDown("down"))
                    currentState = menuState.selectThunderAttack;
                break;

            case menuState.selectThunderAttack:
                fireBallSelector.GetComponent<Renderer>().enabled = false;
                frostBallSelector.GetComponent<Renderer>().enabled = false;
                thunderBallSelector.GetComponent<Renderer>().enabled = true;
                cycloneBallSelector.GetComponent<Renderer>().enabled = false;

                if (Input.GetKeyDown("return"))
                    currentState = menuState.selectTarget;
                if (Input.GetKeyDown("up"))
                    currentState = menuState.selectFrostAttack;
                if (Input.GetKeyDown("down"))
                    currentState = menuState.selectCycloneAttack;
                break;

            case menuState.selectCycloneAttack:
                fireBallSelector.GetComponent<Renderer>().enabled = false;
                frostBallSelector.GetComponent<Renderer>().enabled = false;
                thunderBallSelector.GetComponent<Renderer>().enabled = false;
                cycloneBallSelector.GetComponent<Renderer>().enabled = true;

                if (Input.GetKeyDown("return"))
                    currentState = menuState.selectTarget;
                if (Input.GetKeyDown("up"))
                    currentState = menuState.selectThunderAttack;
                if (Input.GetKeyDown("down"))
                    currentState = menuState.selectFireAttack;
                break;

            case menuState.selectTarget:
                brickSelector.GetComponent<Renderer>().enabled = true;
                brickSelector_menu.GetComponent<Renderer>().enabled = true;

                if(Input.GetKeyDown("return"))
                {
                    if (fireBallSelector.GetComponent<Renderer>().enabled)
                        currentState = menuState.playFireAttack;
                    else if (frostBallSelector.GetComponent<Renderer>().enabled)
                        currentState = menuState.playFrostAttack;
                    else if (thunderBallSelector.GetComponent<Renderer>().enabled)
                        currentState = menuState.playThunderAttack;
                    else if (cycloneBallSelector.GetComponent<Renderer>().enabled)
                        currentState = menuState.playCycloneAttack;
                }
                break;

            case menuState.playFireAttack:
                brickSelector.GetComponent<Renderer>().enabled = false;
                brickSelector_menu.GetComponent<Renderer>().enabled = false;
                fireBallText.enabled = false;
                frostBallText.enabled = false;
                thunderBallText.enabled = false;
                cycloneBallText.enabled = false;
                fireBallSelector.GetComponent<Renderer>().enabled = false;
                frostBallSelector.GetComponent<Renderer>().enabled = false;
                thunderBallSelector.GetComponent<Renderer>().enabled = false;
                cycloneBallSelector.GetComponent<Renderer>().enabled = false;
                attacksListBack.GetComponent<Renderer>().enabled = false;
                fireBall.GetComponent<Renderer>().enabled = true;

                if (!ballBounced)
                {
                    fireBall.transform.position = Vector3.MoveTowards(fireBall.transform.position, GameObject.Find("Paddle").transform.position, Time.deltaTime * 10);
                    if (fireBall.transform.position == GameObject.Find("Paddle").transform.position)
                        ballBounced = true;
                }
                else
                {
                    fireBall.transform.position = Vector3.MoveTowards(fireBall.transform.position, GameObject.Find("Brick").transform.position, Time.deltaTime * 10);
                    if (fireBall.transform.position == GameObject.Find("Brick").transform.position)
                        currentState = menuState.brickDestroyed;
                }
                break;

            case menuState.playFrostAttack:
                brickSelector.GetComponent<Renderer>().enabled = false;
                brickSelector_menu.GetComponent<Renderer>().enabled = false;
                fireBallText.enabled = false;
                frostBallText.enabled = false;
                thunderBallText.enabled = false;
                cycloneBallText.enabled = false;
                fireBallSelector.GetComponent<Renderer>().enabled = false;
                frostBallSelector.GetComponent<Renderer>().enabled = false;
                thunderBallSelector.GetComponent<Renderer>().enabled = false;
                cycloneBallSelector.GetComponent<Renderer>().enabled = false;
                attacksListBack.GetComponent<Renderer>().enabled = false;
                frostBall.GetComponent<Renderer>().enabled = true;

                if (!ballBounced)
                {
                    frostBall.transform.position = Vector3.MoveTowards(frostBall.transform.position, GameObject.Find("Paddle").transform.position, Time.deltaTime * 10);
                    if (frostBall.transform.position == GameObject.Find("Paddle").transform.position)
                        ballBounced = true;
                }
                else
                {
                    frostBall.transform.position = Vector3.MoveTowards(frostBall.transform.position, GameObject.Find("Brick").transform.position, Time.deltaTime * 10);
                    if (frostBall.transform.position == GameObject.Find("Brick").transform.position)
                        currentState = menuState.brickDestroyed;
                }
                break;

            case menuState.playThunderAttack:
                brickSelector.GetComponent<Renderer>().enabled = false;
                brickSelector_menu.GetComponent<Renderer>().enabled = false;
                fireBallText.enabled = false;
                frostBallText.enabled = false;
                thunderBallText.enabled = false;
                cycloneBallText.enabled = false;
                fireBallSelector.GetComponent<Renderer>().enabled = false;
                frostBallSelector.GetComponent<Renderer>().enabled = false;
                thunderBallSelector.GetComponent<Renderer>().enabled = false;
                cycloneBallSelector.GetComponent<Renderer>().enabled = false;
                attacksListBack.GetComponent<Renderer>().enabled = false;
                thunderBall.GetComponent<Renderer>().enabled = true;

                if (!ballBounced)
                {
                    thunderBall.transform.position = Vector3.MoveTowards(thunderBall.transform.position, GameObject.Find("Paddle").transform.position, Time.deltaTime * 10);
                    if (thunderBall.transform.position == GameObject.Find("Paddle").transform.position)
                        ballBounced = true;
                }
                else
                {
                    thunderBall.transform.position = Vector3.MoveTowards(thunderBall.transform.position, GameObject.Find("Brick").transform.position, Time.deltaTime * 10);
                    if (thunderBall.transform.position == GameObject.Find("Brick").transform.position)
                        currentState = menuState.brickDestroyed;
                }
                break;

            case menuState.playCycloneAttack:
                brickSelector.GetComponent<Renderer>().enabled = false;
                brickSelector_menu.GetComponent<Renderer>().enabled = false;
                fireBallText.enabled = false;
                frostBallText.enabled = false;
                thunderBallText.enabled = false;
                cycloneBallText.enabled = false;
                fireBallSelector.GetComponent<Renderer>().enabled = false;
                frostBallSelector.GetComponent<Renderer>().enabled = false;
                thunderBallSelector.GetComponent<Renderer>().enabled = false;
                cycloneBallSelector.GetComponent<Renderer>().enabled = false;
                attacksListBack.GetComponent<Renderer>().enabled = false;
                cycloneBall.GetComponent<Renderer>().enabled = true;

                if (!ballBounced)
                {
                    cycloneBall.transform.position = Vector3.MoveTowards(cycloneBall.transform.position, GameObject.Find("Paddle").transform.position, Time.deltaTime * 10);
                    if (cycloneBall.transform.position == GameObject.Find("Paddle").transform.position)
                        ballBounced = true;
                }
                else
                {
                    cycloneBall.transform.position = Vector3.MoveTowards(cycloneBall.transform.position, GameObject.Find("Brick").transform.position, Time.deltaTime * 10);
                    if (cycloneBall.transform.position == GameObject.Find("Brick").transform.position)
                        currentState = menuState.brickDestroyed;
                }
                break;

            case menuState.brickDestroyed:
                fireBall.GetComponent<Renderer>().enabled = false;
                frostBall.GetComponent<Renderer>().enabled = false;
                thunderBall.GetComponent<Renderer>().enabled = false;
                cycloneBall.GetComponent<Renderer>().enabled = false;
                GameObject.Find("Brick").GetComponent<Renderer>().enabled = false;
                Application.LoadLevel("Game");
                break;
        }
	}
}
