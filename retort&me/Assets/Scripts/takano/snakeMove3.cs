using UnityEngine;
using System.Collections;

public class snakeMove3 : MonoBehaviour {

    public Rigidbody rigidBody = null;

    public bool isPlayer = true;

    public bool snakeMove = false;

	// Use this for initialization
	void Start () {
	
	}



    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (isPlayer == true)
            {
                GetComponent<Renderer>().enabled = true;
                
                snakeMove = true;
                if (snakeMove == true)
                {
                    rigidBody.velocity += new Vector3(2, 0);
                }


            }



        }
    }




    // Update is called once per frame
    void Update () {
	
	}
}
