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

            GetComponent<Rigidbody>().mass = 0;
           
        }

        else
        {
            GetComponent<Rigidbody>().mass = 10;
           
        }
    }


	// Update is called once per frame
	void Update () {

        Barrel();

	}
}
