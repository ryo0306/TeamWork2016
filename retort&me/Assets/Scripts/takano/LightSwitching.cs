using UnityEngine;
using System.Collections;

public class LightSwitching : MonoBehaviour {
    
    public GameObject 仮Enemy = null;

    public bool isLightUp = false;

    // Use this for initialization
    void Start () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "仮Enemy")
        {
            Destroy(仮Enemy);
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
