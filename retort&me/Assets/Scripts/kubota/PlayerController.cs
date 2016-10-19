using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	/// <summary>
	/// The touch start position.
	/// 画面上をタップ（左クリック）したときの位置を取得させる変数
	/// _touchStartPos
	/// 指を画面上から離した時の位置を取得させる変数
	/// _touchEndPos
	/// </summary>
	private Vector3 _touchStartPos;
	private Vector3 _touchEndPos;

	[SerializeField]
	/// <summary>
	/// The direction test.
	/// デザイナーさんたちでも触れるようにSerializeFieldで切っている。
	/// のちに数値が決まり次第それを代入させて使用する。
	/// 現在は仮として30を代入している。
	/// </summary>
	private float _directionTest = 30f;

	string _direction;

	/// <summary>
	/// Flick this instance.
	/// 画面上をタップ（左クリック）したときに
	/// まず_touchStartPosに現在のタップ地点を取得させて
	/// 指を離した時に_touchEndPosにその位置を取得させる関数
	/// </summary>
	void flick(){
		if (Input.GetKeyDown(KeyCode.Mouse0)){
			_touchStartPos = new Vector3(Input.mousePosition.x,
				Input.mousePosition.y,
				Input.mousePosition.z);
		}

		if (Input.GetKey(KeyCode.Mouse0)){
			_touchEndPos = new Vector3(Input.mousePosition.x,
				Input.mousePosition.y,
				Input.mousePosition.z);
			getDirection();
		}

		if (Input.GetKeyUp (KeyCode.Mouse0)) {

			_touchStartPos = Vector3.zero;

			_touchEndPos = Vector3.zero;
			_direction = "null";
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
	/// </summary>
	void getDirection()
	{ 
		float _directionX = _touchEndPos.x - _touchStartPos.x;
		float _directionY = _touchEndPos.y - _touchStartPos.y;

		if (Mathf.Abs (_directionY) < Mathf.Abs (_directionX)) 
		{
			if (_directionTest < _directionX) 
			{
				_direction = "right";
			} 
			else if (-_directionTest > _directionX) 
			{
				_direction = "left";
			}
		} 
		else if (Mathf.Abs (_directionX) < Mathf.Abs (_directionY)) 
		{
			if (_directionTest < _directionY)
			{
				_direction = "jump";
			} 
			else if (-_directionTest > _directionY) 
			{
				_direction = "down";
			}
		} 
		else if (Mathf.Abs (_directionX) == Mathf.Abs (_directionY))
		{
			_direction = "touch";
		} 
		else 
		{
			_direction = "null";
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
	void Update()
	{
		flick ();

		switch (_direction){
		case "jump":
			Debug.Log ("jump");
			break;

		case "down":
			Debug.Log ("down");
			break;

		case "right":
			Debug.Log ("right");
			break;

		case "left":
			Debug.Log ("left");
			break;

		case "touch":
			Debug.Log ("touch");
			break;

		case "null":
			Debug.Log ("null");
			break;
		}

		Debug.Log (_touchStartPos);
		Debug.Log (_touchEndPos);
	}
}