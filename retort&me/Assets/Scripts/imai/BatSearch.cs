using UnityEngine;
using System.Collections;

public class BatSearch : MonoBehaviour
{

    [SerializeField]
    private Enemy_Bat bat;

    void OnTriggerEnter(Collider coll)
    {
		if (coll.gameObject.tag == "Player") {
			bat.InSight ();
		}
    }
}
