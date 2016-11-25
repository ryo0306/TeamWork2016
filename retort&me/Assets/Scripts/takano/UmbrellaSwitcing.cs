using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UmbrellaSwitcing : MonoBehaviour {

    public UmbrellaClosed UmbrellaSwitch;

    // Use this for initialization
    void Start () {

    }


    public void OnClick()
    {
      

        if (UmbrellaSwitch.switching == true)
        {

            UmbrellaSwitch.GetComponent<Renderer>().enabled = false;
            UmbrellaSwitch.switching = false;

        }

        else
        {
            UmbrellaSwitch.GetComponent<Renderer>().enabled = true;
            UmbrellaSwitch.switching = true;
        }




    }


    // Update is called once per frame
    void Update () {
	
	}
}
