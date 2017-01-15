using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class StoryStageData
{
	public string name;

	public StoryPageData[] pages;

}

//ページのデータ
[System.Serializable]
public class StoryPageData
{

	public string name;

	public Texture2D texture;

	public CameraPointData[] points;

}


//カメラが移すポイントごとのデータ
[System.Serializable]
public class CameraPointData
{

	public string name;

	public Vector2 pos;

	public float size;

}