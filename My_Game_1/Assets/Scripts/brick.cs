using UnityEngine;
using System.Collections;

public class brick : MonoBehaviour {

    //called when the ball collides with this object
	//don't destroy brick until after force is applied to ball
    void OnCollisionExit2D(Collision2D other)
    {
		gameController.instance.DestroyBrick(gameObject);
    }
}
