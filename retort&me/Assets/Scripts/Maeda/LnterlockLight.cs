using UnityEngine;
using System.Collections;

public class LnterlockLight : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////

        //連動街灯に付属しています
        //アタッチメント
        //噴水


    ////////////////////////////////////////////////////////////////////////

    [SerializeField]
    private LightSwitch2 lightSwitch2;

    [SerializeField]
    private ChecRange checRange;

    [SerializeField]
    private Switch check;

    void Update()
    {
       
    }

    void OnMouseDown()
    {
       
        if (checRange.canSwitchRenge == true && check.isOn == true)
        {
            //Debug.Log("ccccccc");
            if (lightSwitch2.isLightUp == false)
            {
                lightSwitch2.isLightUp = true;
            }
            else
            {
                lightSwitch2.isLightUp = false;
            }
        }
    }

    
}
