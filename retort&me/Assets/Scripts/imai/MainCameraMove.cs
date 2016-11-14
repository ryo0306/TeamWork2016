using UnityEngine;
using System.Collections;

public class MainCameraMove : MonoBehaviour {

    [SerializeField]
    public GameObject target = null;

    private Vector3 offset = Vector3.zero;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition = target.transform.position + offset;
        transform.position = newPosition;
    }
}

