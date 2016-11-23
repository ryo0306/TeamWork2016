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

        while (target == false)
        {
            yield return null;

            var movDir = pos_ - transform.position;
            movDir.Normalize();
            var mov = movDir * speed;

            if (Mathf.Abs(mov.x) < 0.01f)
            {
                target = true;
            }
            transform.position += mov;
        }

        isMove = false;

        Debug.Log("ムーブ終了");

        StartCoroutine(TurnIterator());


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

            isMove = true;

            //方向を作る

            bool target = false;

            while (target == false)
            {
                yield return null;

                var movDir = startPos - transform.position;
                movDir.Normalize();
                var mov = movDir * speed;

                if (Mathf.Abs(mov.x) < 0.01f)
                {
                    target = true;
                }
                transform.position += mov;
            }

        isMove = false;

        Debug.Log("戻り終了");

    }

    

}
