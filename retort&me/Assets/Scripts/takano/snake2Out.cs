using UnityEngine;
using System.Collections;

public class snake2Out : MonoBehaviour {


    public bool isPlayer = true;

    public Rigidbody rigidBody = null;

    public bool isGround = false;


    // Use this for initialization
    void Start () {
	
	}


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (isPlayer == true)
            {

                rigidBody.velocity += new Vector3(-2.2f, 0);
                rigidBody.isKinematic = false;


            }
        }
        
        if (coll.gameObject.tag == "Ground")
        {
            isGround = true;
            if (isGround == true)
            {

                
                rigidBody.useGravity = false;


            }
        }
        
    }
  






    // Update is called once per frame
    void Update () {
	
	}
}
