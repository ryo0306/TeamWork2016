using UnityEngine;

using System.Collections;

public class Player : MonoBehaviour
{
    Rigidbody rigidBody = null;

    [SerializeField, Range(0.0f,10.0f),Tooltip("移動速度")]
    public float nomalSpeed = 1;

    [SerializeField, Range(0.0f, 10.0f), Tooltip("ダッシュ時の速度")]
    public float dashSpeed = 2;

    //タッチされた位置
    private Vector2 targetPos;

    [SerializeField, Tooltip("開いてる時の空気抵抗")]
    float openedDrag = 10;
    [SerializeField, Tooltip("閉じてる時の空気抵抗")]
    float closedDrag = 0;

    [SerializeField, Tooltip("ジャンプの初速度")]
    float jumpforce = 300;

    [SerializeField, Tooltip("ダッシュになる距離")]
    float dashRange = 3;

    [SerializeField,Tooltip("反応しない距離")]
    private float minRange = 0.1f;

    private Vector2 originPos = Vector2.zero;

    bool jumped = false;

    //一時的なもの
    float DebugTime = 0.0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        targetPos = transform.position;
        originPos = transform.position;
    }

    //ジャンプ
    void Jump()
    {
        
        //本来はダメ？
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumped)
            {
                Debug.Log("can't");
                return;
            }
            rigidBody.AddForce(Vector3.up * jumpforce);
            jumped = true;
            DebugTime = Time.fixedTime;
        }
    }


    /// <summary>
    /// 移動
    /// タッチしている位置とプレイヤーの距離が一定上になると
    /// 歩きから走り状態になる
    /// </summary>
    void Move()
    {
        if (!Input.GetKey(KeyCode.Mouse0)) return;
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (minRange > Mathf.Abs(targetPos.x - transform.position.x)) return;

        Vector3 direction = new Vector3(1,0,0);
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
    }





    void Gravity()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            rigidBody.drag = openedDrag;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            rigidBody.drag = closedDrag;
        }
    }


    public void Dead()
    {

    }

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            if ((coll.transform.position.y + coll.transform.localScale.y / 2) < transform.position.y - transform.localScale.y / 2)
                if(Time.fixedTime - DebugTime > 0.1f)
                jumped = false;
        }
    }

    void OnCollisionExit(Collision coll)
    {

    }

    
    //ここら辺もすべてコルーチン化するべき
    void FixedUpdate()
    {
        if (!GameManager.Instace.isDead)
        {
            Jump();
            Move();
        }
        Gravity();
    }
}