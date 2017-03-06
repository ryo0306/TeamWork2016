using UnityEngine;
using System.Collections;

public class TitleManager : SingletonMonoBehaviour<TitleManager>
{
    void Start()
    {
        BGMManager.Instance.Play("stageselect");
    }

    public void GameaStart()
    {
        FadeManager.Instace.LoadLevel("Story", 2.0f);
        Debug.Log("on");
    }

    public void StageSelect()
    {
        FadeManager.Instace.LoadLevel("StageSelect", 2.0f);
        Debug.Log("on");
    }

    public void Option()
    {
        FadeManager.Instace.LoadLevel("StageSelect", 2.0f);
        Debug.Log("on");
    }
}
