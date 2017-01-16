using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    [SerializeField]
    private ParticleSystem particle = null;

    [SerializeField,Range(0.0f, 10.0f), Tooltip("噴水起動から灯りが点灯するまでの時間")]
    private float waitTime = 5.0f;

    [SerializeField]
    public bool isOn = false;


    void Start ()
    {
        if (particle.isPlaying) particle.Stop();
    }
    
    void fountainstarting()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator kidou()
    {
        while (true)
        {
                yield return new WaitForSeconds(waitTime);
                isOn = true;
            yield return null;
        }
    }

    //スイッチオン
    void OnMouseDown()
    {
        particle.Play();
        StartCoroutine(kidou());
    }
}
