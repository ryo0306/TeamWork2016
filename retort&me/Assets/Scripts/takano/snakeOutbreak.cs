using UnityEngine;
using System.Collections;

public class snakeOutbreak : MonoBehaviour {

    public bool isPlayer = true;

    public Rigidbody rigidBody = null;

    // Use this for initialization
    void Start () {
	
	}


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (isPlayer == true)
            {

                rigidBody.velocity += new Vector3(0, -6);

            }
        }
       
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {

            rigidBody.transform.position = new Vector3(2.6f, -1.2f, 0);

        }
    }


    // Update is called once per frame
    void Update () {
	
	}
}
