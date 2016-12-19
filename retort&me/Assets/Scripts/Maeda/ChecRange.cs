using UnityEngine;
using System.Collections;

public class ChecRange : MonoBehaviour {


    //////////////////////////////////////////////////////////////////////////////////////



        //連動街灯と街灯（　普通　）に使っている
        //playerの範囲のチェックを見ています



    //////////////////////////////////////////////////////////////////////////////////////


    [SerializeField]
    public bool canSwitchRenge = false;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("haitta");
            canSwitchRenge = true;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player") { 

        canSwitchRenge = false;
        }
    }
}
