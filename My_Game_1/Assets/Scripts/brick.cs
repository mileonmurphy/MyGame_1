using UnityEngine;
using System.Collections;

public class brick : MonoBehaviour {

    //public GameObject myBrick;
	protected float destroyTimer = 0f;
	protected float timeUntilDestroy = -1f;
	protected gameController gc;

	void Start(){
		//myBrick = this.gameObject;
		gc = FindObjectOfType<gameController>();
	}

	void Update() {
		// allow the ball to collide with the brick by not destroying it instantly
		if (timeUntilDestroy > 0) {
			// update timer
			destroyTimer += Time.deltaTime;
			if (destroyTimer > timeUntilDestroy) {
				print ("brick pos: " + transform.position);
				gc.Guy.rebuildBrick (transform.position);
				gc.DestroyBrick ();
				Destroy (this.gameObject);
			}
		}
	}

    //called when the ball collides with this object
    void OnCollisionEnter2D(Collision2D other)
    {
		timeUntilDestroy = 0.1f;
		//gameController.instance.DestroyBrick();


    }
}
