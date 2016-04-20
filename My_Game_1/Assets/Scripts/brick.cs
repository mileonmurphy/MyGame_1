using UnityEngine;
using System.Collections;

public class brick : MonoBehaviour {

    public GameObject myBrick;

    //called when the ball collides with this object
    void OnCollisionEnter(Collision other)
    {
        Instantiate(myBrick, transform.position, Quaternion.identity);
        gameController.instance.DestroyBrick();
        Destroy(gameObject);
    }
}
