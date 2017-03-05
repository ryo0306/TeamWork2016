using UnityEngine;
using System.Collections;

public class SpotLight : MonoBehaviour {

    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void OnTriggerStay(Collider coll)
    {
        if (!light.enabled) return;
        Debug.Log(coll.name);
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            Destroy(coll.gameObject);
        }
    }
}
