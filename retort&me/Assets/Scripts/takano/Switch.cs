using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    [SerializeField]
    public GameObject fountain;
    
    public GameObject Player = null;
    //LightSwitchingを取っている
    public LightSwitching Lightfire;

    [SerializeField]
    public bool switchings = false;

    // Use this for initialization
    void Start ()
    {  
        StartCoroutine(kidou());
    }
    
    void fountainstarting()
    {
        fountain.SetActive(true);
    }

    private IEnumerator kidou()
    {
        while (true)
        {
            if (switchings == true)
            {
                yield return new WaitForSeconds(5.0f);

                Lightfire.isLightUp = true;
            }
            yield return null;
        }
    }

    void OnMouseDown()
    {
        fountainstarting();
        switchings = true;
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
  
    void Update ()
    {

    }
}
