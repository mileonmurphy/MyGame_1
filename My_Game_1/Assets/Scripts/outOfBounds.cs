using UnityEngine;
using System.Collections;

public class outOfBounds : MonoBehaviour {

void OnTriggerEnter2D(Collider2D other)
    {
        gameController.instance.loseLife();
    }
}
