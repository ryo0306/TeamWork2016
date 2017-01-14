using UnityEngine;

using System.Collections;



public class Player : MonoBehaviour
{
   
    public Vector3 defaultScale = Vector3.zero;

    public Rigidbody rigidBody = null;

    public bool gravity = true;

    public UmbrellaClosed umbrellaFlag;

    [SerializeField, Range(0.0f, 10.0f), Tooltip("移動速度")]
    public float nomalSpeed = 1;

    [SerializeField, Range(0.0f, 10.0f), Tooltip("ダッシュ時の速度")]
    public float dashSpeed = 2;

    //タッチされた位置
    private Vector2 targetPos;

    [SerializeField, Tooltip("ジャンプの初速度")]
    float jumpForce = 300;

    [SerializeField, Tooltip("ダッシュになる距離")]
    float dashRange = 3;

    [SerializeField, Tooltip("反応しない距離")]
    private float minRange = 0.1f;

    private Vector2 originPos = Vector2.zero;

    bool jumped = false;

    public bool squat = true;

    public bool isGround;

    public bool isDrag = true;
    

    [SerializeField]
    public float drag;

    //一時的なもの
    float DebugTime = 0.0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        transform.position = GameManager.Instace.startPos;
        targetPos = transform.position;
        originPos = transform.position;
        defaultScale = transform.lossyScale;



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
            rigidBody.AddForce(Vector3.up * jumpForce);
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
    }

  

    void Gravity()
    {
      
            if (umbrellaFlag.switching == true)
            {
            
                 jumpForce = 300;
                 dashRange = 3;
            }
            else
            {         
                jumpForce = 0;
                dashRange = 100;

            if(isGround == false)
            {
                if (isDrag == true)
                {
                    Debug.Log("aaaa");
                    rigidBody.velocity = Vector3.zero;
                    isDrag = false;
                }
                
                Debug.Log("bbbb");
                rigidBody.velocity += new Vector3(0,drag);
            }
            
          
            }
        }
    
    void Hiding()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (umbrellaFlag.switching == false && squat == false)
            {

                Debug.Log("隠れている");
                

            }
            else
            {
                Debug.Log("隠れてない");
               
            }
        }
    }

    void Squat()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (squat == true)
            {
                Debug.Log("しゃがむ");
                squat = false;
            }

            else
            {
                Debug.Log("しゃがんでない");
                squat = true;
            }
        }
        
    }


    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            if ((coll.transform.position.y + coll.transform.localScale.y / 2) < transform.position.y - transform.localScale.y / 2)
                if(Time.fixedTime - DebugTime > 0.1f)
                jumped = false;
            isGround = true;
            isDrag = true;
        }
       
    }


    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            
            isGround = false;

        }

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
        Squat();
        Hiding();
    }
}