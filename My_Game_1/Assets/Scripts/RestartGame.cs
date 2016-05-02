using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

public void ResetMe()
	{
		Application.LoadLevel("Title");
	}
}
