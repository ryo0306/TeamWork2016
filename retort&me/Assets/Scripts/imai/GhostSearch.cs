using UnityEngine;
using System.Collections;

public class GhostSearch : MonoBehaviour {

    [SerializeField]
    private Ghost ghost;


    void OnTriggerStay(Collider coll){
        if (coll.gameObject.tag == "Player")
            ghost.InSight(coll.transform.position);
    }

    void OnTriggerExit(Collider coll){
        if (coll.gameObject.tag == "Player")
            ghost.Missing();
    }
}
