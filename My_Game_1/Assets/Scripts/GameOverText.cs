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
		myText.text = "Score: " + gameController.score.ToString ();
	}

	void Update()
	{
		//testButton = Input.GetKey ();
		if(testButton != null)
		{
			Application.LoadLevel("Title");
		}
	}
}
