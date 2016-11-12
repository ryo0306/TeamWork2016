using UnityEngine;
using System.Collections;

public class SpiderYarn : MonoBehaviour {

    //くもの糸

    //ついてる先 SpiderYarn

    [SerializeField]
    Vector3 PlayerSpeed = new Vector3(0, 0, 0);

    [SerializeField]
    GameObject spidertracking;

    [SerializeField]
    private bool his_yarn = false;


    // Use this for initialization
    void Start () {

        PlayerSpeed = transform.position;


    }
	
	// Update is called once per frame
	void Update () {
        
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            if (his_yarn == false)
            {
                Instantiate(spidertracking);
                his_yarn = true;
                Debug.Log("当たった");
                Debug.Log("追尾クモでたよ");

            }
        }
    }
}
