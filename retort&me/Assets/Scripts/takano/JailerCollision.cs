using UnityEngine;
using System.Collections;

public class JailerCollision : MonoBehaviour {

    public bool appear = false;


    [SerializeField]
    GameObject SponerObject;

    [SerializeField]
    GameObject SponerEnemyPrefab;




    // Use this for initialization
    void Start () {
	
	}


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            appear = true;
            var obj = (GameObject)Instantiate(SponerEnemyPrefab);
            obj.transform.position = SponerObject.transform.position;

            Debug.Log(appear);
        }

    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            appear = false;
            Debug.Log(appear);
        }
    }


        // Update is called once per frame
        void Update () {
	
	}
}
