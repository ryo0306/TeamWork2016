using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	/// <summary>
	/// The touch start position.
	/// 画面上をタップ（左クリック）したときの位置を取得させる変数
	/// touchStartPos
	/// 指を画面上から離した時の位置を取得させる変数
	/// touchEndPos
	/// </summary>
	private Vector3 touchStartPos;
	private Vector3 touchEndPos;

	[SerializeField]
	/// <summary>
	/// The direction test.
	/// デザイナーさんたちでも触れるようにSerializeFieldで切っている。
	/// のちに数値が決まり次第それを代入させて使用する。
	/// </summary>
	private float ranningDirection = 150f;
	[SerializeField]
	private float walkingDirection = 100f;

	string direction;
	/// <summary>
	/// The test flag.
	/// 傘を開いている状態と閉じている状態があるため
	/// それを切り替えるための仮フラグを用意。
	/// </summary>
	[SerializeField]
	private bool testFlag = false;

	/// <summary>
	/// Flick this instance.
	/// 画面上をタップ（左クリック）したときに
	/// まずtouchStartPosに現在のタップ地点を取得させて
	/// 指を離した時にtouchEndPosにその位置を取得させる関数
	/// </summary>
	void PuniCon ()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			touchStartPos = new Vector3 (Input.mousePosition.x,
				Input.mousePosition.y,
				Input.mousePosition.z);
		}

		if (Input.GetKey (KeyCode.Mouse0)) {
			touchEndPos = new Vector3 (Input.mousePosition.x,
				Input.mousePosition.y,
				Input.mousePosition.z);
			GetDirection (testFlag);
		}

		if (Input.GetKeyUp (KeyCode.Mouse0)) {

			touchStartPos = Vector3.zero;

			touchEndPos = Vector3.zero;
			direction = "null";
		}
	}

	/// <summary>
	/// Gets the direction.
	/// X軸とY軸それぞれの移動量を取得し
	/// それらを使用してif文で処理を切っている。
	/// 
	/// １．x,yの絶対値で見たときにどっちが多いかでの分岐
	/// →上下のフリックor左右のフリック
	/// 2．移動量の判定（マイナスかプラスか）
	/// →上下or左右のどちらかを判定
	/// 3．どれにも当てはまらない場合はタッチとみなして処理する。
	/// 
	/// なお、上部のtestFlagは外部から引っ張って管理させる形が好ましい。
	/// 用意したGetDirectionの引数に突っ込めば動かせる
	/// 
	/// </summary>
	void GetDirection (bool _umbrellaFlag)
	{ 
		float directionX = touchEndPos.x - touchStartPos.x;
		float directionY = touchEndPos.y - touchStartPos.y;

		Debug.Log (directionY);
		Debug.Log (directionX);

		if (Mathf.Abs (directionY) < Mathf.Abs (directionX)) {
			if (walkingDirection < directionX) {
				direction = "walkRight";
			} else if (-walkingDirection > directionX) {
				direction = "walkLeft";
			} 
			if (ranningDirection < directionX) {
				if(_umbrellaFlag == false)
					direction = "ranRight";
			} 
			else if (-ranningDirection > directionX) {
				if(_umbrellaFlag == false)
					direction = "ranLeft";
			}
		} else if (Mathf.Abs (directionX) < Mathf.Abs (directionY)) {
			if (ranningDirection < directionY) {
				direction = "jump";
			} else if (-ranningDirection > directionY) {
				direction = "down";
			}
		} else if (Mathf.Abs (directionX) == Mathf.Abs (directionY)) {
			direction = "touch";
		} else {
			direction = "null";
		}

	}

	/// <summary>
	/// Update this instance.
	/// 注文にあったコルーチンを使用しての制作であったが
	/// やり方がいまいちわからないためこのままUpdateを使用しての
	/// 処理確認をさせてもらった。
	/// 何かの処理をさせるときに各ケース文の中に代入してやれば
	/// 動くはず。
	/// </summary>
	void Update ()
	{
		PuniCon ();

		switch (direction) {

		case "jump":
			Debug.Log ("jump");
			break;

		case "down":
			Debug.Log ("down");
			break;

		case "walkRight":
			Debug.Log ("walkRight");
			break;

		case "ranRight":
			Debug.Log ("ranRight");
			break;

		case "walkLeft":
			Debug.Log ("walkLeft");
			break;

		case "ranLeft":
			Debug.Log ("ranLeft");
			break;

		case "touch":
			Debug.Log ("touch");
			break;

		case "null":
			Debug.Log ("null");
			break;
		}
	}
}