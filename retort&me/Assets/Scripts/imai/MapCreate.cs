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

    //本体分けるべきではない
    [SerializeField]
    public int stageNum = 0;


    protected override void Awake() 
    {
        base.Awake();
        //初期位置を微調整
        originPos += new Vector2(0.5f, 0.5f);
        DontDestroyOnLoad(this.gameObject);
    }

    public void Load()
    {
        data = MapLoad.Instance.Load(dataPath);
        data.Dump();
        Debug.Log(data.width);
        Debug.Log(data.height);
    }

    public void Create()
    {
       
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

                Instantiate(temp);
            }
        }
        GameObject temp2 = Instantiate(stageGimmick[stageNum - 1]);
        if (temp2 == null)
        {
            Debug.Log("ギミックが生成されなかったんだよな…");
        }
        else
        {
            Debug.Log(temp2.name + "が生成されました");
        }
        Debug.Log(data.tileHeight);
        Debug.Log(data.tileWidth);

    }
}
