using UnityEngine;

using System.Collections;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField, Tooltip("移動速度")]
    public float playerSpeed = 2;

    private Vector2 pos;
   

    [SerializeField, Tooltip("開いてる時の空気抵抗")]
    float openedDrag = 10;
    [SerializeField, Tooltip("閉じてる時の空気抵抗")]
    float closedDrag = 0;


    [SerializeField, Tooltip("ジャンプの初速度")]
    float jumpforce = 300;

    void Start()
    {
        pos.x = -7f;

        pos.y = -4f;

        rb = GetComponent<Rigidbody>();
    }
    //ジャンプ

    void PlayerJump()
    {
        if (Input.GetKeyDown("space"))

        {
            rb.AddForce(Vector3.up * jumpforce);
        }

    }


    //移動

    void Player_Move()
    {

        if (Input.GetMouseButton(0))

        {

            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //this.transform.position += new Vector3(player_speed, 0, 0);
            //現在地からtouchした場所までの指定
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, pos.y),
                                                     playerSpeed * Time.deltaTime);
        }
    }

    void Player_Dash()
    {
        if (Input.GetMouseButton(1))

        {

            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //this.transform.position += new Vector3(player_speed, 0, 0);
            //現在地からtouchした場所までの指定
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, pos.y),
                                                     playerSpeed * Time.deltaTime * 2);
        }
    }



    void PlayerGravity()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            rb.drag = openedDrag;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            rb.drag = closedDrag;
        }


    }

    void Update()
    {

        Player_Move();
        PlayerJump();
        PlayerGravity();
        Player_Dash();


    }

    
}