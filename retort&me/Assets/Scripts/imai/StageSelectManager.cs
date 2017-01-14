using UnityEngine;
using System.Collections;

public class StageSelectManager : SingletonMonoBehaviour<StageSelectManager>
{

    void Start()
    {
        BGMManager.Instance.Play("stageselect");
    }

    public void Selected(int stagenum)
    {
        PublicData.Instace.stageNum = stagenum;
        SceneEnd();
    }

    public void SceneEnd()
    {
        FadeManager.Instace.LoadLevel("MainGame", 2.0f);
        Debug.Log("on");
    }

    public void Return()
    {
        FadeManager.Instace.LoadLevel("Title", 2.0f);
    }

}