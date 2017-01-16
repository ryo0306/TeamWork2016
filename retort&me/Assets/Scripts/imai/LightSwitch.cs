using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {


    [SerializeField]
    Switch switch_;

    public bool lightOn = false;

    [SerializeField]
    Light light_;

    void Start()
    {
        StartCoroutine(Updater());
    }
    
    IEnumerator Updater()
    {
        while (true)
        {
            //毎回見るのはよくない
             light_.enabled = (lightOn && switch_.isOn);
            
        yield return null;
        }
    }

    void OnMouseDown()
    {
        lightOn = !lightOn;
    }
}
