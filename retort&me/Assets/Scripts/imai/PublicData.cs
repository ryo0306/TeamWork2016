using UnityEngine;
using System.Collections;

public class PublicData : SingletonMonoBehaviour<PublicData> {

    public int stageNum { get; set; }

    struct SaveData
    {
        int finalStage;
    }

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        stageNum = 1;
    }

}
