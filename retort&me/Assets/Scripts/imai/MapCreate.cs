using UnityEngine;
using System.Collections;

public class MapCreate : SingletonMonoBehaviour<MapCreate> {

    /// <summary>
    /// 一時的な参照
    /// 完成したらすべてファイルから読み込む
    /// </summary>
    [SerializeField]
    GameObject []ground = null;

    [SerializeField]
    GameObject[] stageGimmick = null;
    
    MapLoad.Layer2D data = null;

    [SerializeField,Tooltip("マップの初期位置")]
    public Vector2 originPos = Vector2.zero;

    [SerializeField,Tooltip("読み込むXML")]
    public string dataPath = null;

    protected override void Awake() 
    {
        base.Awake();
        //初期位置を微調整
        originPos += new Vector2(0.5f, 0.5f);
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    public void Load()
    {
        dataPath = "stage" + PublicData.Instace.stageNum;
        data = MapLoad.Instance.Load(dataPath);
        data.Dump();
        Debug.Log(data.width);
        Debug.Log(data.height);
    }



    public void Create()
    {
        //改善案：マップチップを用意あたり判定用と描写用にわける
        //**********************************************************************************
        //データはground1_1.○○○で用意してもらう
        //ground(床の種類)_(ステージ番号)
        Debug.Log(dataPath + "を生成中");
        for (int y = 0; y < data.height; y++)
        {
            for (int x = 0; x < data.width; x++)
            {
               
                if (data.Get(x, y) == 0) continue;
                GameObject temp = null;
                if (ground.Length < data.Get(x, y))
                {
                    temp = ground[7];
                }
                else
                {
                    temp = ground[data.Get(x, y) - 1];
                }

                if (data.Get(x, y) == 15)
                {
                    GameManager.Instace.startPos = new Vector3(originPos.x + x, originPos.y + data.height - y+1, 0);
                }

                

                temp.transform.position = new Vector3(originPos.x + x, originPos.y + data.height - y, 0);
                temp.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Texture/MapChip/ground" + data.Get(x, y) + "_" + PublicData.Instace.stageNum);
                Instantiate(temp);
            }
        }
        //***************************************************************************:
        //ギミックのprefabの生成
        Debug.Log(PublicData.Instace.stageNum);
        GameObject gimmicks = Instantiate(stageGimmick[PublicData.Instace.stageNum - 1]);
        if (gimmicks == null)
        {
            Debug.Log("ギミックが生成されませんでした。");
        }
        else
        {
            Debug.Log(gimmicks.name + "が生成されました");
        }
        Debug.Log(data.tileHeight);
        Debug.Log(data.tileWidth);

    }
}
