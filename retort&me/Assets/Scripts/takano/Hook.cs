using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {

    public UmbrellaClosed HookFrag;

    public Player player;


    // Use this for initialization
    void Start () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (HookFrag.switching == true && transform.position.x > player.transform.position.x)
            {
                player.rigidBody.velocity += new Vector3(4, 0);
            }
            if (HookFrag.switching == true && transform.position.x < player.transform.position.x)
            {
                player.rigidBody.velocity += new Vector3(-4, 0);
            }
        }
    }




    // Update is called once per frame
    void Update () {
	
	}
}
