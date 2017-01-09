using UnityEngine;
using System.Collections;

public class CreatePopupUI : MonoBehaviour {

	[SerializeField]
	GameObject PopupPrefab;

	[SerializeField]
	bool TestmyUIFlag = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (TestmyUIFlag == false) {
			ClosePopup ();
		} else {
			OpenPopup ();
		}
	}

	/// <summary>
	/// Opens the popup.
	/// 仮に制作しておきました。
	/// ここで呼び出された時の処理をする
	/// </summary>
	void OpenPopup()
	{
		PopupPrefab.SetActive (true);
	}

	/// <summary>
	/// Closes the popup.
	/// 仮に制作しておきました。
	/// ここで閉じられた時の処理をする。
	/// </summary>
	void ClosePopup()
	{
		PopupPrefab.SetActive (false);
	}
}
