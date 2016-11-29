using UnityEngine;
using System.Collections;

public class SpiderYarn : MonoBehaviour {

    //くもの糸

    //ついてる先 SpiderYarn

    [SerializeField]
    GameObject spidertracking;


    [SerializeField]
    public bool hitYarn = false;


    // Use this for initialization
    void Start () {



    }
	
	// Update is called once per frame
	void Update () {
        
    }


    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Player"){
            hitYarn = true;
            //Debug.Log("当たった");
        }
    }

    void OnTriggerExit(Collider collider) { 
        if (collider.gameObject.tag == "Player"){
            hitYarn = false;
            //Debug.Log("当たらなくなった");
        }
    }

}
