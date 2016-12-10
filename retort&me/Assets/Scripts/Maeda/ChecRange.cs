using UnityEngine;
using System.Collections;

public class ChecRange : MonoBehaviour {


    [SerializeField]
    public bool canSwitchRenge = false;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("haitta");
            canSwitchRenge = true;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player") { 

        canSwitchRenge = false;
        }
    }
}
