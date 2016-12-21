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
        MapCreate.Instace.stageNum = stagenum;
        MapCreate.Instace.dataPath = "stage" + stagenum;
        SceneEnd();
    }

    public void SceneEnd()
    {
        FadeManager.Instace.LoadLevel("MainGame", 2.0f);
        Debug.Log("on");
    }

}