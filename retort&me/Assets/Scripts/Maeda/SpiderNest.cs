using UnityEngine;
using System.Collections;

public class SpiderNest : MonoBehaviour {


    //クモの巣

    //ついてる先　　　SpiderNest 

    [SerializeField]
    GameObject spider;

    [SerializeField]
     private bool hit_nest = false;

    [SerializeField]
    private Vector3 spiderpos;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player"){
            if (hit_nest == false) {
                Instantiate(spider,new Vector3(spiderpos.x, spiderpos.y,spiderpos.z), Quaternion.identity);
                hit_nest = true;
                Debug.Log("当たった");
                Debug.Log("クモでたよ");

            }

        }
    }

}
