using UnityEngine;
using System.Collections;

public class snakeBody : MonoBehaviour {


    public bool isSnakePosition = false;

    public Rigidbody rigidBody = null;
    public bool isGround = false;

    // Use this for initialization
    void Start () {

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isGround = true;
            if (isGround == true)
            {


                rigidBody.useGravity = false;
                rigidBody.velocity += new Vector3(-2f, 0);

            }

        }
        if (coll.gameObject.tag == "Wall")
        {
            isGround = false;
            if (isSnakePosition == true)
            {
                
                isSnakePosition = false;
            }
        }
            if (coll.gameObject.tag == "Sall")
            {
                if (isSnakePosition == false)
                {
                   
                    isSnakePosition = true;
                }
            }

        


    }
    void wandering()
    {
        if(isSnakePosition == true)
        {
            rigidBody.velocity += new Vector3(-0.1f, 0);
        }
        if(isSnakePosition == false)
        {
            rigidBody.velocity += new Vector3(0.1f, 0);
        }


    }





    // Update is called once per frame
    void Update () {

        wandering();






    }
}
