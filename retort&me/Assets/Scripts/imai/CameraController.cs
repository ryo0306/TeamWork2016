using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("追従するGameObject")]
    public GameObject target;
    [SerializeField, Tooltip("滑らかさ")]
    public float smoothing = 5f;

    private Vector3 offset;
    void Awake()
    {
        if (target == null)
        {
            Debug.LogErrorFormat("targetにするオブジェクトが指定されていません。");
            Debug.LogFormat("よって一時的に自身のGameObjectを入れます。");
            target = this.gameObject;
        }
        offset = transform.position - target.transform.position;
    }

    //コルーチンに
    void FixedUpdate()
    {
        Vector3 targetCamPos = target.transform.position + offset;
        Vector3 temp =  Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.position = new Vector3(temp.x, temp.y ,transform.position.z);

        //ここのマジックナンバーを修正
        if (transform.position.x <= 0)
        {
            transform.position = new Vector3(0,transform.position.y,transform.position.z);
        }

        if (transform.position.x >= 50)
        {
            transform.position = new Vector3(50, transform.position.y, transform.position.z);
        }
    }
}