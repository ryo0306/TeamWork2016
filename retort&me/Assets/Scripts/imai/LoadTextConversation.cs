using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class LoadTextConversation : MonoBehaviour 
{
	public struct ScenariosData
	{
		public string command;				//命令コマンド

		public List<string> sentences;		//本文
	}

	enum ElementNames
	{
		command = 0,						//命令コマンド
		sentences							//描画する本文
	}

	const string COMMAND_SENTENCE = "Sentence";

	private ScenariosData[] scenariosData;

	public ScenariosData[] ScenariosDatas
	{
		get{ return scenariosData;}
	}

	/// <summary>
	/// The CSVDAT a ELEMENT.
	/// CSVデータの要素数
	/// </summary>
	private const int CSVDATA_ELEMENTS = 2;

	private string[] scenarioDictionary = {
		"Story.csv"
	};

	string[] didCommaSeparationData;

	/// <summary>
	/// Reads the file.
	/// ファイルの読み込みをしている関数
	/// 第一引数　読み込みたいシナリオの名前を入力
	/// 
	/// </summary>
	/// <param name="dictionaryNumber_">Dictionary number.</param>
	void ReadFile(int dictionaryNumber_)
	{
		string path = Application.dataPath + "" + scenarioDictionary [dictionaryNumber_];

		string[] lines = ReadCsvFoundation.ReadCsvData (path);

		didCommaSeparationData = new string[lines.Length]; 

		char[] commaSplitter = { ',' };

		int count = 0;
		string commandStorageTemp;

		for(int i = 0; i < lines.Length; i++)
		{
			commandStorageTemp = didCommaSeparationData [(int)ElementNames.command];

			if (commandStorageTemp != "") {
				count += 1;
			}
		}

		scenariosData = new ScenariosData[count];

		for(int i = 0; i< lines.Length; i++)
		{
			didCommaSeparationData = DataSeparation (lines [i], commaSplitter, CSVDATA_ELEMENTS - 1);

			scenariosData [i].command = didCommaSeparationData [(int)ElementNames.command];

			if (scenariosData [i].command != COMMAND_SENTENCE) {
				StorageAll (i);
			}
			else if(scenariosData[i].command == "")
			{
				ForEmptyCommand (i);
			}

		}
	}

	void ForEmptyCommand(int elementNum_)
	{
		if (scenariosData [elementNum_ - 1].command == COMMAND_SENTENCE) {
			AdditionalStorage (elementNum_ - 1);
		} else if (scenariosData [elementNum_ - 1].command == "") {
			AdditionalStorage (elementNum_ - 2);
		}
	}

	void AdditionalStorage(int elementNum_)
	{
		scenariosData [elementNum_].sentences.Add (didCommaSeparationData [(int)ElementNames.sentences]);
	}

	void StorageAll(int elementNum_)
	{
		scenariosData [elementNum_].sentences.Add (didCommaSeparationData [(int)ElementNames.sentences]);
	}

	string[] DataSeparation(string lines_, char[] spliter_, int trialNumber_)
	{
		string[] CommaSeparationData = new string[trialNumber_];
		for (int i = 0; i < trialNumber_; i++) {
			string[] readStrData = new string[trialNumber_];
			readStrData = lines_.Split (spliter_);
			CommaSeparationData [i] = readStrData [i];
		}
		return CommaSeparationData;
	}

	void Start()
	{

	}
}
