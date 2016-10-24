using UnityEngine;

using System.Collections;



public class PlayerMove : MonoBehaviour
{



    [SerializeField]

    public float player_speed = 2;

    Vector2 vec;



    Rigidbody rb;

    [SerializeField]

    float jumpforce = 300;

    // Use this for initialization

    void Start()

    {

        vec.x = -7f;

        vec.y = -4f;



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

            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //現在地からtouchした場所までの指定

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(vec.x, vec.y),

               player_speed * Time.deltaTime);

        }



    }









    void Update()

    {



        Player_Move();

        PlayerJump();



    }





}