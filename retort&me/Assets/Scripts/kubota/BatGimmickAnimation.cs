using UnityEngine;
using System.Collections;

public class BatGimmickAnimation : MonoBehaviour {

	[SerializeField]
	public static bool TestMyFlag = false;

	// Use this for initialization
	void Start () {
		
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// 高野の作ったスクリプト内で使用。
	/// 判定にタグのついたオブジェクトが入ったら、作動するようになっている。
	/// 街灯の光部分には"Light"のタグをつけてもらい、傘のオブジェクトには"Umbrella"をタグ付けしてもらう。
	/// FIXED：もしかしたら、スピードが速いとすり抜けてしまう可能性がある。
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Light") {
			TestMyFlag = true;
		}
		else if (other.gameObject.tag == "Umbrella") {
			TestMyFlag = true;
		}
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (TestMyFlag);
	}
}
