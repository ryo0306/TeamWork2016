﻿using UnityEngine;
using System.Collections;



public class Bezier : MonoBehaviour
{
    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    public float ti = 0f;

    private Vector3 b0 = Vector3.zero;
    private Vector3 b1 = Vector3.zero;
    private Vector3 b2 = Vector3.zero;
    private Vector3 b3 = Vector3.zero;

    private float Ax;
    private float Ay;
    private float Az;

    private float Bx;
    private float By;
    private float Bz;

    private float Cx;
    private float Cy;
    private float Cz;

    // Init function v0 = 1st point, v1 = handle of the 1st point , v2 = handle of the 2nd point, v3 = 2nd point
    // handle1 = v0 + v1
    // handle2 = v3 + v2
    public Bezier(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        this.p0 = v0;
        this.p1 = v1;
        this.p2 = v2;
        this.p3 = v3;
    }
    // 0.0 >= t <= 1.0
    public Vector3 GetPointAtTime(float t)
    {
        this.CheckConstant();
        float t2 = t * t;
        float t3 = t * t * t;
        float x = this.Ax * t3 + this.Bx * t2 + this.Cx * t + p0.x;
        float y = this.Ay * t3 + this.By * t2 + this.Cy * t + p0.y;
        float z = this.Az * t3 + this.Bz * t2 + this.Cz * t + p0.z;
        return new Vector3(x, y, z);
    }

    private void SetConstant()
    {
        this.Cx = 3f * ((this.p0.x + this.p1.x) - this.p0.x);
        this.Bx = 3f * ((this.p3.x + this.p2.x) - (this.p0.x + this.p1.x)) - this.Cx;
        this.Ax = this.p3.x - this.p0.x - this.Cx - this.Bx;
        this.Cy = 3f * ((this.p0.y + this.p1.y) - this.p0.y);
        this.By = 3f * ((this.p3.y + this.p2.y) - (this.p0.y + this.p1.y)) - this.Cy;
        this.Ay = this.p3.y - this.p0.y - this.Cy - this.By;

        this.Cz = 3f * ((this.p0.z + this.p1.z) - this.p0.z);
        this.Bz = 3f * ((this.p3.z + this.p2.z) - (this.p0.z + this.p1.z)) - this.Cz;
        this.Az = this.p3.z - this.p0.z - this.Cz - this.Bz;
    }

    // Check if p0, p1, p2 or p3 have change
    private void CheckConstant()
    {
        if (this.p0 != this.b0 || this.p1 != this.b1 || this.p2 != this.b2 || this.p3 != this.b3)
        {
            this.SetConstant();
            this.b0 = this.p0;
            this.b1 = this.p1;
            this.b2 = this.p2;
            this.b3 = this.p3;
        }
    }
}




/// <summary>
/// FIXED　高野：コメントを利用すること
/// 			 これはチーム制作であって、個人制作ではない。
/// </summary>
public class Enemy_Bat : MonoBehaviour {
    [HideInInspector]
    public Bezier myBezier;
    [SerializeField]
    public float t = 0f;

	[SerializeField]
	public bool HitFlag = false;

    [SerializeField,Range(0.000f,0.010f)]
    public float speed = 0.008f;

    [SerializeField, Tooltip("Enemyの始点")]
    private Vector2 Starting;
    [SerializeField, Tooltip("Enemyの終点")]
    private Vector2 End;
    [SerializeField, Tooltip("Enemyの中点１")]
    private Vector2 Middle1;
    [SerializeField, Tooltip("Enemyの中点２")]
    private Vector2 Middle2;

    public bool bat = false;

    void Start()
    {
        Starting = transform.position;
        End += new Vector2(transform.position.x, transform.position.y);

        //4点のポジションを取っている
        myBezier = new Bezier(new Vector2(Starting.x, Starting.y), new Vector2(Middle1.x, Middle1.y), new Vector2(Middle2.x, Middle2.y), new Vector2(End.x, End.y));
    }



    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameManager.Instace.Dead();
        }
    }


    public void InSight()
    {
        bat = true;
    }

    void EnemyMove()
    {
		
			if (bat == true) {
				Vector3 vec = myBezier.GetPointAtTime (t);
				transform.position = new Vector3(vec.x,vec.y,transform.position.z);

				t += speed;
           
			}


        if (t > 1.0f)
        {
            Destroy(this.gameObject);
        }

    }

	/// <summary>
	/// Gimmicks the hits.
	///追加点。
	///別のスクリプトを参照している。
	///現状、演出を加えたかったが、色の変化などがうまくできなかったため
	///保留としている。
	///FIXED：ただ、壊すだけではなく、何かしらの動作をさせてから壊す。
	/// </summary>
	void GimmickHits()
	{
		if (BatGimmickAnimation.TestMyFlag == true) {
			Destroy (this.gameObject, 3.0f);
		}
	}

    void Update()
    {
        EnemyMove();
		//デバッグのために読み込んでいる。
		//他にいい方法があるかもしれないが現状はこれで動作確認する。
		GimmickHits ();
    }
}











