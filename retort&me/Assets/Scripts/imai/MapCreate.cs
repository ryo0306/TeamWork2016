using UnityEngine;
using System.Collections;

public class MapCreate : MonoBehaviour {

    /// <summary>
    /// 一時的な参照
    /// 完成したらすべてファイルから読み込む
    /// </summary>
    [SerializeField]
    GameObject []ground = null;
    
    MapLoad.Layer2D data = null;

    [SerializeField]
    public Vector2 originPos = Vector2.zero;

    [SerializeField]
    private string dataPath = null;



    void Start()
    {
        //初期位置を微調整
        originPos += new Vector2(0.5f, 0.5f);
        Load();
        Create();
    }

    void Load()
    {
        data = MapLoad.Instance.Load(dataPath);
    }

    void Create()
    {
        
        for (int y = 0; y < data.height; y++)
        {
            for (int x = 0; x < data.width; x++)
            {
                if (data.Get(x, y) == 0) continue;


                GameObject temp = ground[data.Get(x, y)-1];
                temp.transform.position = new Vector3(originPos.x + x, originPos.y + data.height - y, 0);
                Instantiate(temp);
            }
        } 
    }
}
