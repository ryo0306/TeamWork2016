using UnityEngine;
using System.Collections;

public class Spide_Controller : MonoBehaviour {

    //クモの追尾
    //SpiderTracking　プレハブ名

    //追いかけるターゲット
    private Transform target;
    // 回転速度
    public float rotMax;
    //敵のスピード
    private float speed = 0.1f;

    Vector3 move = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {
   
     //ゲームオブジェクト名で確認している　↓
            target = GameObject.Find("Player").transform;

    }
	
	// Update is called once per frame
	void Update () {

        // ターゲットの方向を向く
        Vector3 vec = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, vec.y, 0)), rotMax);
        // 正面方向に移動
        transform.Translate(Vector3.forward * speed); 
    }


}
