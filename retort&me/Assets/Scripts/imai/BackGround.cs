using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {

    [SerializeField, Tooltip("camera")]
    public GameObject mainCamera;



    private Vector3 offset;

    void Start()
    {
        offset = transform.position - mainCamera.transform.position;
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);
    }
}
