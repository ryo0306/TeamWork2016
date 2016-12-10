using UnityEngine;
using System.Collections;

public class CageSwitch : MonoBehaviour
{

    public bool isPlayer = false;

    public bool isCageTrap = false;


    void OnMouseDown()
    {
        if (isPlayer == true)
        {
            isCageTrap = true;
        }
    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isPlayer = true;


         
        }
    }





}