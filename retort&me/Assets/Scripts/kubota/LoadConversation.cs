using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.IO;

public class LoadConversation : MonoBehaviour {

	/// <summary>
	/// The sentences.
	/// csvを格納するため箱
	/// </summary>
	private string[] sentences;

	/// <summary>
	/// Gets the sentences.
	/// 
	/// どこでも引っ張れるように
	/// ゲッターを用意
	/// </summary>
	/// <value>The sentences.</value>
	public string[] Sentences
	{
		get { return sentences;}
	}

	/// <summary>
	/// The scenario dictionary.
	/// 表示するcsvのデータをここで必要な分だけ登録して格納する。
	/// </summary>
	private string[] scenarioDictionary = {
		"Story1.csv",
		"Story2.csv",
		"Story3.csv",
		"Story4.csv",
		"Story5.csv",
		"Story6.csv",
		"Story7.csv",
	};

	/// <summary>
	/// The did comma separation data.
	/// コンマ区切りされたデータを格納する箱
	/// </summary>
	string[] didCommaSeparationData;

	// Use this for initialization
	void Start () 
	{
		///一旦、これで読み込んでいる。
		/// 今後、ここを変えるかもしれない。
		//for(int i = 0; i < 7; i++)
		//{
		//	ReadFile (i);
		//}
	}

	/// <summary>
	/// Reads the file.
	/// 呼び込むことで、読み込みを始める。
	/// 引数にはscenarioDictionaryから表示させるものを番号で指定する。
	/// ０～最大数までを指定する。
	/// </summary>
	/// <param name="dictionaryNumber_">Dictionary number.</param>
	void ReadFile(int dictionaryNumber_)
	{

		//Assetの一番上までを見ている状態。
		//その後、自分でフォルダーの場所を指定してあげている。
		//FIXED：場所が変わるのなら、""の中身を変化させる必要あり。
		string path = Application.dataPath + "/Scenes/Kubota/CSVStorys/" + scenarioDictionary [dictionaryNumber_];
		
		string[] lines = ReadCsvFoundation.ReadCsvData (path);

		didCommaSeparationData = new string[lines.Length];

		char[] commaSplitter = { ',' };

		sentences = new string[lines.Length];

		//データをそれぞれの箱に格納している場所
		for (int i = 0; i < lines.Length; i++) {


			didCommaSeparationData = ReadCsvFoundation.DataSeparation(lines [i], commaSplitter, 1);
		
			sentences[i] = didCommaSeparationData[0];

			//Debug.Log (sentences [i]);
		
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
