using UnityEngine;
using System.Collections;

//やるべき事
//○見やすく！
//○本を開くなどの演出がほしい
//○シナリオに行くときに演出をいれる



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
        FadeManager.Instace.LoadLevel("Story", 2.0f);
        Debug.Log("on");
    }

    public void Return()
    {
        FadeManager.Instace.LoadLevel("Title", 2.0f);
    }

}