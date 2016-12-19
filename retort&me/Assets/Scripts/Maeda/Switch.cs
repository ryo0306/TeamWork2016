using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {


    //////////////////////////////////////////////////////////////////////////////////////


    //噴水に付属しています


    //////////////////////////////////////////////////////////////////////////////////////

    [SerializeField,Tooltip("対象のlight")]
    public LightSwitching[] Lightfire;

    [SerializeField]
    private ParticleSystem particle = null;

    [SerializeField,Range(0.0f, 10.0f), Tooltip("噴水起動から灯りが点灯するまでの時間")]
    private float waitTime = 5.0f;

    [SerializeField]
    public bool isOn = false;

    // Use this for initialization
    void Start ()
    {
        if (particle.isPlaying) particle.Stop();
        StartCoroutine(kidou());
    }
    
    void fountainstarting()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator kidou()
    {
        while (true)
        {
            if (isOn == true)
            {
                particle.Play();
                yield return new WaitForSeconds(waitTime);
                /*foreach (var l in Lightfire)
                {
                    l.isLightUp = false;
                }*/
            }
            yield return null;
        }
    }

    void OnMouseDown()
    {
        Debug.Log("aaaaaaaaaaa");
        fountainstarting();
        isOn = true;
    }
    /*
    void OnTriggerEnter(Collider coll)
    {

        if (coll.gameObject.tag == "Player")
        {

            if (Input.GetMouseButton(0))
            {
                //fountainstarting();
                switchings = true;      
            }
        }   
    }
    */
  
}
