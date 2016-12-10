using UnityEngine;
using System.Collections;

public class LnterlockLight : MonoBehaviour {

    [SerializeField]
    private LightSwitching lightSwitching;

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
