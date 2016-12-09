using UnityEngine;
using System.Collections;

public class CageTrap : MonoBehaviour {

    public Rigidbody rigidBody = null;

    public CageSwitch S;

    public bool isCageFrag;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "仮Enemy")
        {
            isCageFrag = true;
            if(isCageFrag == true) { 
            
                Destroy(coll.gameObject);
                }
            }
    }

    void G()
    {
        if(S.isCageTrap == true)
        {
            rigidBody.isKinematic = false;
            rigidBody.velocity += new Vector3(0, -3);
            S.isCageTrap = false;
        }

    }


   
    // Update is called once per frame
    void Update () {
        G();

    }
}
