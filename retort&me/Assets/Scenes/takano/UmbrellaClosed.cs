using UnityEngine;
using System.Collections;

public class UmbrellaClosed : MonoBehaviour {
    [SerializeField]
    public bool switching;


    //public Renderer renderer;

    //GameObject game_object = GameObject.Find("Sphere");


    // Use this for initialization
    void Start()
    {
        switching = true;

    }

    void PlayerUmbrella()
    {

        //押している時は表示して離したら切り替わる
        if (Input.GetKey(KeyCode.Z))
        {
            if (switching == true)
            {
                GetComponent<Renderer>().enabled = false;
                switching = false;

            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {

            //if (switching == false)
            //{
            GetComponent<Renderer>().enabled = true;
            switching = true;
            //}

        }


    }



    // Update is called once per frame
    void Update()
    {


        PlayerUmbrella();
        //Debug.Log(switching);

    }
}
