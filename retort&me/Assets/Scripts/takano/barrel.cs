using UnityEngine;
using System.Collections;

public class barrel : MonoBehaviour {

    public UmbrellaClosed UmbrellaFlags;
    
    // Use this for initialization
    void Start () {

    }
	
    void Barrel()
    {
        if (UmbrellaFlags.switching == true)
        {

            GetComponent<Rigidbody>().isKinematic = false;
           
        }

        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
           
        }
    }


	// Update is called once per frame
	void Update () {

        Barrel();

	}
}
