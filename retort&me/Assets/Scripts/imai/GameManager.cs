using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehaviour<GameManager> {

    protected override void Awake()
    {
        base.Awake();
        MapCreate.Instace.Load();
        MapCreate.Instace.Create();
    }

    void Start()
    {
        BGMManager.Instance.Play("stageselect");
    }

    public bool isDead = false;

    public Vector3 startPos = Vector3.zero;

    public void Dead()
    {
        FadeManager.Instace.LoadLevel("MainGame", 4.0f);
        isDead = true;
    }

    public void SceneEnd()
    {
        FadeManager.Instace.LoadLevel("StageSelect", 2.0f);
        Debug.Log("on");
    }

}
