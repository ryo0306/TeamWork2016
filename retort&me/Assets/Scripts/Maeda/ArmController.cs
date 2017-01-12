using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour {

    [SerializeField]
    float speed;

    [SerializeField]
    float timeCorrection;

    float angleLimit;
    float time;






	// Use this for initialization
	void Start () {

        //speed = 100.0f;
        angleLimit = 60f;
        time = 0f;


	}
	
	// Update is called once per frame
	void Update () {

        var angle  = Mathf.Cos( time * timeCorrection ) * speed;

        var rot = Quaternion.Euler(0,0,angle);

        transform.localRotation = rot;  

        time += Time.deltaTime;

	}
}
