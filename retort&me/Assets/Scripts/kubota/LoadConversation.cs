using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.IO;

public class LoadConversation : MonoBehaviour {

	private string[] sentences;

	public string[] Sentences
	{
		get { return sentences;}
	}

	private string[] scenarioDictionary = {
		"Sample1.csv",
		"Sample2.csv",
		"Sample3.csv",
	};

	string[] didCommaSeparationData;

	// Use this for initialization
	void Start () 
	{
		ReadFile (0);
	}

	void ReadFile(int dictionaryNumber_)
	{
		string path = Application.dataPath + "/Scenes/Kubota/CSVStorys/" + scenarioDictionary [dictionaryNumber_];
		
		string[] lines = ReadCsvFoundation.ReadCsvData (path);

		didCommaSeparationData = new string[lines.Length];

		char[] commaSplitter = { ',' };

		sentences = new string[lines.Length];

		for (int i = 0; i < lines.Length; i++) {


			didCommaSeparationData = ReadCsvFoundation.DataSeparation(lines [i], commaSplitter, 1);
		
			sentences[i] = didCommaSeparationData[0];

			Debug.Log (sentences [i]);
		
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
