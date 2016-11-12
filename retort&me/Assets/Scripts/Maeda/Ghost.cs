using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

    private Transform playertarget;
    public float rotMax;
    private float speed = 0.1f; 

    Vector3 move = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {

        //ゲームオブジェクト名で確認している　↓
        playertarget = GameObject.Find("Player").transform;
        
        
    }
	
	// Update is called once per frame
	void Update () {

        
    }

    
    void OnTriggerStay(Collider collider){

        if (collider.gameObject.name == "Player")
        {
            // ターゲットの方向を向く
            Vector3 vec = playertarget.position - transform.position;
           transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, 0)), rotMax);
            // 正面方向に移動
            transform.Translate(Vector3.forward * speed);
            Debug.Log("当たった");
        }

    }
    






 }
