
/*
読み込むデータ
ex)
Message string pos
//ここまでは最低限できるように
Action Player Move tagetpos
Create Boss pos
Action Boss Pos
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CSVReader
{
    private static CSVReader instance = new CSVReader();

    public static CSVReader Instance
    {
        get
        {
            return instance;
        }
    }

    private CSVReader()
    {

    }

        public List<string[]> Read(string filePath_)
    {

        // ローカルフォルダにあるデータを読み込むには、file:// をパスの先頭につける
        var path = "file://" + Application.streamingAssetsPath + "/EventData/" + filePath_;
        Debug.Log(path);

        // 指定したパスにあるデータを読み込む
        // 本来はインターネット上にあるデータを取得するものだが、
        // ローカルフォルダを指定することもできる
        WWW www = new WWW(path);
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(www.error);
            return null;
        }

        //読み込みが終わるまで待機
        while (!www.isDone) { }

        string temp = www.text;
        string[] lines = temp.Replace("\r\n", "\n").Split("\n"[0]);
       

        
        List<string[]> texts = new List<string[]>();

        //確認用
        foreach (var line in lines)
        {
            if (line == "") { continue; }
            texts.Add(line.Split(","[0]));
            Debug.Log(line);
        }

        Debug.Log("finish");
        return texts;
    }
}