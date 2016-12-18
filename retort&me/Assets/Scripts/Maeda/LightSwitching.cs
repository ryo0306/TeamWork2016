using UnityEngine;
using System.Collections;

public class LightSwitching : MonoBehaviour {


    ///////////////////////////////////////////
    

    //街灯（　普通　）のライトに付属しています


    ///////////////////////////////////////////


    public bool isLightUp = false;

    // Use this for initialization
    void Start () {
	
	}


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            Destroy(coll.gameObject);
        }
    }



    void Switching()
    {
        if (isLightUp == false)
        {
            GetComponent<Light>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }

        if (isLightUp == true)
        {
            
            GetComponent<Light>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }
 


    void Update () {
        Switching();
    }
}
