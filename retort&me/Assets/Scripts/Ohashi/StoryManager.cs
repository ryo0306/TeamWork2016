using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ストーリーシーンを管理するクラス
//・シングルトン
public class StoryManager : MonoBehaviour
{

	static StoryManager Instance { get; set; }

	[SerializeField, Tooltip("使うカメラ")]
	Camera camera;

	[SerializeField, Tooltip("カメラの移動のデータ")]
	StoryPageData pageData;

	[SerializeField, Tooltip("カメラの移動のデータ(ステージごと)")]
	StoryPageData[] pagesData;

	[SerializeField, Tooltip("カメラの移動速度")]
	float cameraSpeed;

	[SerializeField]
	RawImage image;

	//カメラが何番目の座標にいるor行くところか
	int pointCount;

	//カメラが移動中かどうか
	public bool IsMove { get; private set; }

	//カメラが回転中かどうか
	public bool IsRotate { get; private set; }

	//カメラを振る状態かどうか
	public bool IsShake { get; private set; }

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	void Start()
	{
		//カメラの初期化
		//camera.orthographicSize = 540f;
		//camera.transform.position = Vector3.zero;

		pointCount = -1;

		Setup();

		StartCoroutine(UpdataMain());

	}

	void Update()
	{
		
	}

	//シーンのメインループをコルーチンで管理
	IEnumerator UpdataMain()
	{

		while(true)
		{
			yield return 0;

			//左クリック　カメラの遷移
			if (Input.GetMouseButtonDown(0))
			{
				if (pointCount < pageData.points.Length - 1 && IsMove == false)
				{
					StartCoroutine(MoveCameraCoruetine());
				}

				//次のシーンへ
				if(pointCount >= pageData.points.Length)
				{
					Shutdown();
				}
			}

			//右クリック(デバッグ:回転)
			if (Input.GetMouseButtonDown(1))
			{
				StartCoroutine(RotateCamera(480f, 3));
			}

			//Sキー(デバッグ揺れる)
			if (Input.GetKeyDown(KeyCode.S))
			{
				//shakeCamera.Shake();
				StartCoroutine(ShakeCamera(15f, 3.0f));
			}

		}

	}

	/// <summary>
	/// 初期化
	/// </summary>
	void Setup()
	{
		camera.orthographicSize = 540f;
		camera.transform.position = Vector3.zero;

		image.texture = pageData.texture;
		pointCount = -1;
	}

	/// <summary>
	/// 移動終了後
	/// </summary>
	void Shutdown()
	{

	}

	/// <summary>
	/// カメラを動かすコルーチン
	/// </summary>
	/// <returns></returns>
	IEnumerator MoveCameraCoruetine()
	{
		pointCount++;
		IsMove = true;

		//目標の座標
		var endPos = pageData.points[pointCount].pos;

		//目標座標までの距離の計算
		var camPos2D = (Vector2)camera.transform.position;
		var endDistVec = endPos - camPos2D;
		var endDist = endDistVec.magnitude;

		//移動方向の計算
		var dir = endDistVec.normalized;

		//総移動距離
		float totalDist = 0.0f;

		//最初のカメラの描画サイズ
		var beginSize = camera.orthographicSize;

		//最終的なカメラの描画サイズ
		var endSize = pageData.points[pointCount].size;

		//カメラの変動する描画サイズ
		var diffSize = endSize - beginSize;

		while (true)
		{
			yield return null;

			//今フレームの移動距離
			float movDist = cameraSpeed * Time.deltaTime;

			//カメラを移動させる
			camera.transform.position += (Vector3)dir * movDist;

			//移動させた分だけ移動した距離に加算
			totalDist += movDist;
			//Debug.Log("総移動距離 " + totalDist );

			//カメラサイズの変更
			camera.orthographicSize = beginSize + (diffSize * totalDist / endDist);

			//終了条件
			if(totalDist >= endDist )
			{
				camera.transform.position = new Vector3(endPos.x, endPos.y, -1f);
				camera.orthographicSize = endSize;
				break;
			}

		}

		IsMove = false;
	}

	IEnumerator ShakeCamera(float amount, float time)
	{
		if (IsShake) yield break;
		IsShake = true;

		var totalTime = 0.0f;

		var originPos = camera.transform.position;

		while (true)
		{
			yield return null;

			var mov = new Vector3(
				Random.Range(-amount, amount),
				Random.Range(-amount, amount),
				-1f);

			camera.transform.position = originPos + mov;

			totalTime += Time.deltaTime;

			//終了条件
			if (totalTime >= time )
			{
				break;
			}
		}

		yield return null;

		camera.transform.position = originPos;

		IsShake = false;
	}


	/// <summary>
	/// カメラを回転させるコルーチン
	/// </summary>
	/// <param name="rotSpd">回転速度</param>
	/// <param name="rotTimes">回転回数</param>
	/// <returns></returns>
	IEnumerator RotateCamera(float rotSpd, int rotTimes)
	{
		if (IsRotate) yield break;
		IsRotate = true;

		float endAngle = 360f * (float)rotTimes;
		float totalAngle = 0f;

		while( true )
		{
			yield return null;

			//今フレーム
			var angle = Time.deltaTime * rotSpd;

			//カメラの回転
			camera.transform.Rotate(0,0,angle);

			//回転した量
			totalAngle += angle;

			Debug.Log("総回転量 " + totalAngle );
			
			//終了条件
			if (Mathf.Abs(totalAngle) > Mathf.Abs(endAngle) )
			{
				Debug.Log("回転終了");
				camera.transform.rotation = Quaternion.identity;
				break;
			}
		}
		IsRotate = false;
	}
}