using UnityEngine;
using System.Collections;

public class Ropeway : MonoBehaviour {

    public UmbrellaClosed ropeway;

    public Player player;


	void Start () {
	
	}

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (ropeway.switching == true)
            {
                player.rigidBody.useGravity = false;
                player.rigidBody.velocity += new Vector3(0.1f, 0);
            }
            else
            {          
                player.rigidBody.useGravity = true;   
            }
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            player.rigidBody.useGravity = true;

            player.rigidBody.velocity = Vector3.zero;       
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
