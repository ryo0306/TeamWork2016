using UnityEngine;
using System.Collections;

public class StreetLightSwitch : MonoBehaviour
{


    ///////////////////////////////////////////
    
    //普通の街灯に使ってます。
    //playerが範囲内にいるか確認しています。


    ///////////////////////////////////////////


    [SerializeField]
    private LightSwitching lightSwitching;

    [SerializeField]
    private ChecRange checRange;

    void OnMouseDown()
    {
        if (checRange.canSwitchRenge == true) {
            if (lightSwitching.isLightUp == false)
            {
                lightSwitching.isLightUp = true;
            }
            else
            {
                lightSwitching.isLightUp = false;
            }
        }
    }


}
