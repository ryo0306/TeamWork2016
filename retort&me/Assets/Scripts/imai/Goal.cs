using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.Instace.SceneEnd();
        }
    }
}
