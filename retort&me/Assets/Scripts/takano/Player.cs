using UnityEngine;
using System.Collections;

//やるべき事
//○傘の設計の書き直し
//○しゃがむなどの本実装
//○死亡時のアニメーション
//○傘をさしている時の行動パターンの実装

//出来たらやる事
//○ジャンプがしにくい問題の解消
//移動はaddforthよりトランスフォーム？
public class Player : MonoBehaviour
{

    [SerializeField,Tooltip("Player")]
    public Rigidbody rigidBody = null;

    public bool gravity = true;

    [SerializeField, Range(0.0f, 10.0f), Tooltip("移動速度")]
    public float nomalSpeed = 1;

    [SerializeField, Range(0.0f, 10.0f), Tooltip("ダッシュ時の速度")]
    public float dashSpeed = 2;

    [SerializeField, Tooltip("ジャンプの初速度")]
    float jumpForce = 300;

    [SerializeField, Tooltip("ダッシュになる距離")]
    float dashRange = 3;

    [SerializeField, Tooltip("反応しない距離")]
    private float minRange = 0.1f;

    [SerializeField, Tooltip("空気抵抗(傘開いている時の落ちる速度)")]
    float drag = 0;

    private Vector2 originPos = Vector2.zero;

    [SerializeField]
    bool jumped = false;

    public bool umbrellaIsOpen = false;

    [SerializeField, Tooltip("しゃがんでいるかどうか")]
    public bool squat = false;

    [SerializeField]
    Animator animator;

    [SerializeField,Tooltip("開いた瞬間にどこまで減速するか(大きいほど遅くならない)"),Range(0.0f,1.0f)]
    float openedDragLimit = 0.3f;

   

    //一時的なもの
    float DebugTime = 0.0f;
    [SerializeField, Tooltip("開いた瞬間にかかる空気抵抗(除算)")]
    float opendDrag = 3;

    //タッチされた位置
    private Vector2 targetPos;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        transform.position = GameManager.Instace.startPos;
        targetPos = transform.position;
        originPos = transform.position;
    }

    //ジャンプ
    public void Jump()
    {
        //傘を開いてる場合はジャンプできない
        if (umbrellaIsOpen)
        {
            Debug.Log("傘を開いているためジャンプできません。");
            return;
        }

            if (jumped) return;
            Debug.Log("jumped");
            rigidBody.AddForce(Vector3.up * jumpForce);
            jumped = true;
            DebugTime = Time.fixedTime;

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    if (jumped) return;
        //    Debug.Log("jumped");
        //    rigidBody.AddForce(Vector3.up * jumpForce);
        //    jumped = true;
        //    DebugTime = Time.fixedTime;
        //}
    }


    /// <summary>
    /// 移動
    /// タッチしている位置とプレイヤーの距離が一定上になると
    /// 歩きから走り状態になる
    /// </summary>
    void Move()
    {
        Vector2 pos = transform.position;


        // 画面左下のワールド座標をビューポートから取得
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // プレイヤーの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;


        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

        if (squat) return;
        if (!Input.GetKey(KeyCode.Mouse0)) return;

        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (minRange > Mathf.Abs(targetPos.x - transform.position.x)) return;

        Vector3 direction = new Vector3(1, 0, 0);
        if (targetPos.x - transform.position.x > 0) direction = new Vector3(1, 0, 0);
        else direction = new Vector3(-1, 0, 0);


        if (Mathf.Abs(targetPos.x - transform.position.x) < dashRange)
        {
            //一時的にマジックナンバー
            if (Mathf.Abs(rigidBody.velocity.x) > nomalSpeed) return;
            rigidBody.AddForce(direction * 10, ForceMode.Force);
        }
        else
        {
            if (Mathf.Abs(rigidBody.velocity.x) > dashSpeed) return;
            rigidBody.AddForce(direction * 10, ForceMode.Force);
        }
    

        //アニメーション
        //移動量が一定量を超えたら走るモーション
        animationUpdate();
    }

    void MoveLimit()
    {
        //画面外に出ないように
    }


    void umbrellaOpenMove()
    {
        if (umbrellaIsOpen)
        {
            rigidBody.velocity += new Vector3(0, drag);
        }
    }
    
    void Hiding()
    {
    }

    void Squat()
    {
        squat = false;
        if(Input.GetKey(KeyCode.H))
        {
            squat = true;
        }
        animator.SetBool("Squat",squat);
        //しゃがんだかどうかの判定はanimatorのSquatをみてください；
        animator.GetBool("Squat");
    }


    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            if(Mathf.Abs(rigidBody.velocity.y) <= Mathf.Abs(0.0f))
                    jumped = false;
        }
    }

    void animationUpdate()
    {
        Quaternion q = new Quaternion(0, 0, 0, 0);
        if (rigidBody.velocity.x > 0.1)
        {
            q.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = q;
        }
        else if(rigidBody.velocity.x < -0.1)
        {
            q.eulerAngles = new Vector3(0,180,0);
            transform.rotation = q;
        }
    }

    //ここら辺もすべてコルーチン化するべき?
    void FixedUpdate()
    {
        if (!GameManager.Instace.isDead)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Jump();
            }
            Move();
        }
        umbrellaOpenMove();
        Squat();
        Hiding();
        
        //MoveLimit();
    }

    /// <summary>
    /// **傘の開閉を変える関数です。**
    /// 傘を変える関数ではありません。
    /// </summary>
    public void UmbrellaChage()
    {
        umbrellaIsOpen = !umbrellaIsOpen;
        animator.SetBool("IsOpen", umbrellaIsOpen);
        StartCoroutine(OpendDrag());
    }


    //傘を開いた瞬間に加わる瞬間的な空気抵抗
    private IEnumerator OpendDrag()
    {
        while (Mathf.Abs(rigidBody.velocity.y) > openedDragLimit)
        {
            Debug.Log("減速中");
           rigidBody.velocity /= opendDrag;
           yield return null;
        }
        yield return null;
    }
}