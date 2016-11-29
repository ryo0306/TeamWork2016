using UnityEngine;
using System.Collections;

public class Spide_Controller : MonoBehaviour
{
    [SerializeField]
    SpiderYarn[] spiderYarn;

    //movコルーチンが呼ばれているか否か確認
    bool isMove;

    [SerializeField]
    private float spiderSpeed;

    //bool mov;

        //振り向く時間
    [SerializeField]
    float turnTime;


    [SerializeField]
    Vector3 startPos;



    // Use this for initialization
    void Start(){

        //startPos = new Vector3();
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //衝突判定の確認
        for (int i = 0; i < spiderYarn.Length; ++i)
        {
            if (spiderYarn[i].hitYarn == true && isMove == false)
            {
                Debug.Log(" spiderHit : " + i);
                var pos = spiderYarn[i].transform.position;

                StartCoroutine(Move(pos, spiderSpeed));
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player"){
            Debug.Log("プレイヤーを見つけたよ");
        }
    }

    //クモを移動させるコルーチン
    //コルーチンは終了するまで動く処理

    IEnumerator Move(Vector3 pos_, float speed){

        isMove = true;

        //方向を作る


        Debug.Log("Move");

        bool target = false;
        //変数名決める
        bool movCheck = false;

        float movDir_ = pos_.x - transform.position.x;
        //movDir.Normalize();

        if (movDir_ >= 0)
        {
            movCheck = true;
        }
        else
        {
            movCheck = false;
        }

        //Xだけにしました。
        while (target == false)
        {
            yield return null;
            //座標とpositionを計算している
            float movDir = pos_.x - transform.position.x;
            //movDir.Normalize();

            if (movCheck == true)
            {
                float mov = speed;
                //　Abs絶対値　
                if (movDir <= 0f)
                {
                    target = true;
                    Debug.Log("もうお家帰るｗｗ");
                }

                transform.position += new Vector3(mov, 0, 0);

            }
            else
            {
                float mov = -speed;
                //　Abs絶対値　
                if (movDir >= 0f)
                {
                    target = true;
                    Debug.Log("もうお家帰る1000");
                }
                transform.position += new Vector3(mov, 0, 0);
            }
        }

        float pos_y = transform.position.y;
        transform.position = new Vector3 (pos_.x,pos_y, 0);

        StartCoroutine(TurnIterator());

        //isMove = false;

        Debug.Log("ムーブ終了");

    }

    //振り向きするコルーチン
    IEnumerator TurnIterator()
    {
        yield return new WaitForSeconds(turnTime);
        transform.Rotate(0, 180, 0);

        yield return new WaitForSeconds(turnTime);
        transform.Rotate(0, 180, 0);

        yield return new WaitForSeconds(turnTime);
        transform.Rotate(0, 180, 0);

        Debug.Log("ターン終了");

        StartCoroutine(positionTrun(spiderSpeed));

    }

    IEnumerator positionTrun(float speed) {

            //isMove = true;

            //方向を作る

            bool target = false;
        /*
            while (target == false)
            {
                yield return null;

            float movDir = startPos.x - transform.position.x;
            //movDir.Normalize();

            if (movDir >= 0)
            {
                movDir = 1;
            }
            else
            {
                movDir = -1;
            }

            float mov = movDir * speed;

            if (Mathf.Abs(mov) < 1f)
            {
                target = true;
            }
            transform.position += new Vector3 (mov, 0, 0);
            
                var movDir = startPos - transform.position;
                //movDir.Normalize();
                var mov = movDir * speed;
                
                if (Mathf.Abs(mov.x) < 0.01f)
                {
                    target = true;
                }
                transform.position += mov;
           
        }
          */

        bool movCheck = false;

        float movDir_ = startPos.x - transform.position.x;
        //movDir.Normalize();

        if (movDir_ >= 0)
        {
            movCheck = true;
        }
        else
        {
            movCheck = false;
        }

        //Xだけにしました。
        while (target == false)
        {
            yield return null;
            //座標とpositionを計算している
            float movDir = startPos.x - transform.position.x;
            //movDir.Normalize();

            if (movCheck == true)
            {
                float mov = speed;
                //　Abs絶対値　
                if (movDir <= 0f)
                {
                    target = true;
                    Debug.Log("もうお家帰るｗｗ");
                }

                transform.position += new Vector3(mov, 0, 0);

            }
            else
            {
                float mov = -speed;
                //　Abs絶対値　
                if (movDir >= 0f)
                {
                    target = true;
                    Debug.Log("もうお家帰る");
                }
                transform.position += new Vector3(mov, 0, 0);
            }
        }

        float pos_y = transform.position.y;
        transform.position = new Vector3(startPos.x, pos_y, 0);


        Debug.Log("戻り終了");

        isMove = false;
        
        }
    }
