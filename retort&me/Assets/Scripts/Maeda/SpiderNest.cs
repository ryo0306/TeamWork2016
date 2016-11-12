using UnityEngine;
using System.Collections;

public class SpiderNest : MonoBehaviour {


    //クモの巣

    //ついてる先　　　SpiderNest 

    [SerializeField]
    GameObject spider;

    [SerializeField]
     private bool hit_nest = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "Player"){
            if (hit_nest == false) {
                Instantiate(spider);
                hit_nest = true;
                Debug.Log("当たった");
                Debug.Log("クモでたよ");
            }

        }
    }

}
