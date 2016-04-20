using UnityEngine;
using System.Collections;

public class brick : MonoBehaviour {

    public GameObject myBrick;

	void Start(){
		myBrick = this.gameObject;
	}

    //called when the ball collides with this object
    void OnCollisionEnter2D(Collision2D other)
    {
        gameController.instance.DestroyBrick();
        Destroy(myBrick);
    }
}
