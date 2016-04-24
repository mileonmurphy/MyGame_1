using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour {

	public GameObject myTextObject;
	public Text myText;
	public string testButton = null;
	// Use this for initialization
	void Start () {
		myText = myTextObject.GetComponent<Text> ();
		print (gameController.score);
		myText.text = "Score: " + gameController.score.ToString ();
	}

	public void LoadMenu ()
	{
		Application.LoadLevel("Title");
	}
}
