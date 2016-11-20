using UnityEngine;
using System.Collections;

public class UmbrellaClosed : MonoBehaviour {
    [SerializeField]
    public bool switching;

    // Use this for initialization
    void Start()
    {
        switching = true;

    }

    void PlayerUmbrella()
    {

        //押すと開閉が切り替わる

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (switching == true)
            {
                GetComponent<Renderer>().enabled = false;
                switching = false;

            }

            else
            {
                GetComponent<Renderer>().enabled = true;
                switching = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


        PlayerUmbrella();
        

    }
}
