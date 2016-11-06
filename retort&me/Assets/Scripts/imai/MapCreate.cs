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

    void Start()
    {
        data = MapLoad.Instance.Load("stage2");
        Create();
    }


    void Create()
    {
        data.Dump();
        for (int y = 0; y < data.height; y++)
        {
            for (int x = 0; x < data.width; x++)
            {
                if (data.Get(x, y) == 0) continue;


                GameObject temp = ground[data.Get(x, y)-1];
                temp.transform.position = new Vector3(x, data.height - y, 0);
                Instantiate(temp);
            }
        } 
    }
}
