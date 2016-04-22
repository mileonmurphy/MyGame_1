using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

void OnClick()
	{
		Application.LoadLevel("Title");
	}
}
