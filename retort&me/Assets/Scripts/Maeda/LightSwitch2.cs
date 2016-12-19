using UnityEngine;
using System.Collections;

public class LightSwitch2 : MonoBehaviour
{


    ////////////////////////////////////////////////////////////////////////////////////////////////


    //    LnterlockLight で使っている
    //　　 噴水の連動をみています。　アタッチメントとして噴水を付けえてください


    ////////////////////////////////////////////////////////////////////////////////////////////////



    [SerializeField]
    Switch fountainSwitch;

    public bool isLightUp = false;

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
        if (isLightUp == false || fountainSwitch.isOn == false)
        {
            GetComponent<Light>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }

        if (isLightUp == true && fountainSwitch.isOn == true)
        {
             
            GetComponent<Light>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }



    void Update()
    {
        Switching();
    }
}
