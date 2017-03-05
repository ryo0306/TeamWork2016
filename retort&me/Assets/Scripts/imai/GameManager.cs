using UnityEngine;
using System.Collections;


//やるべき事
//○ポーズの実装
//○イベントマネージャーもこっちで管理するべき
//○セーブデータの作成
//○ギミック全般の実装
//○アニメーションの実装
//○SEManagerの汎用性をあげる
// →BGMManagerのように作る
//○リザルト作成
//○シーンフェイドアウト、フェイドインを多くする
//○インターファイスの改良
//○エネミーの生成の仕方を改めて考え直す
//○背景も2枚にする
//○SEを入れる

//出来たら
//演出を凝る（抽象的なので後でちゃんと決める）
//フックを作成
//




public class GameManager : SingletonMonoBehaviour<GameManager> {

    protected override void Awake()
    {
        base.Awake();
        MapCreate.Instace.Load();
        MapCreate.Instace.Create();
    }

    void Start()
    {
        BGMManager.Instance.Play("Stage5");
    }

    public bool isDead = false;

    public Vector3 startPos = Vector3.zero;

    public bool isPause{ get; set; }

    public bool isEvent{ get; set; }

    public void Dead()
    {
        FadeManager.Instace.LoadLevel("MainGame", 4.0f);
        isDead = true;
    }

    public void Pause()
    {
        isPause = true;
        Debug.Log("Pause");
        Instantiate((GameObject)Resources.Load("Prefabs/PauseUIPrefab1"));
    }

    public void SceneEnd()
    {
        FadeManager.Instace.LoadLevel("StageSelect", 2.0f);
        Debug.Log("on");
    }

}
