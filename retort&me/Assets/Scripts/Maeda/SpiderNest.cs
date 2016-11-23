using UnityEngine;
using System.Collections;

public class SpiderNest : MonoBehaviour {


    //クモの巣

    //ついてる先　　　SpiderNest 

    [SerializeField]
    GameObject spider;

    [SerializeField]
     private bool hit_nest = false;

    [SerializeField,Tooltip("クモの出現場所の位置を設定")]
    private Vector3 spiderPosition;

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
                Instantiate(spider,new Vector3(spiderPosition.x, spiderPosition.y, spiderPosition.z), Quaternion.identity);
                hit_nest = true;
                Debug.Log("当たった");
                Debug.Log("クモでたよ");

            }

        }
    }

}
