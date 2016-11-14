using UnityEngine;
using System.Collections;

public class StageSelectManager : SingletonMonoBehaviour<StageSelectManager>
{

    void Start()
    {
        BGMManager.Instance.Play("stageselect");
    }

    public void SceneEnd()
    {
        FadeManager.Instace.LoadLevel("MainGame", 2.0f);
        Debug.Log("on");
    }

}