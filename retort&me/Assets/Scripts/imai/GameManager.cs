using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehaviour<GameManager> {

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            FadeManager.Instace.LoadLevel("MainGame",2.0f);
    }

}
