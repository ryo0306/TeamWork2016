using UnityEngine;
using System.Collections;

public class TitleManager : SingletonMonoBehaviour<TitleManager>
{
    void Start()
    {
        BGMManager.Instance.Play("stageselect");
    }

    public void SceneEnd()
    {
        FadeManager.Instace.LoadLevel("StageSelect", 2.0f);
        Debug.Log("on");
    }
}
