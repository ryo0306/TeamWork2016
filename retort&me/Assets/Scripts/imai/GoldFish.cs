using UnityEngine;
using System.Collections;

public class GoldFish : MonoBehaviour {

    [SerializeField]
    GameObject target = null;
    Rigidbody rigidBody = null;

    [SerializeField]
    float hommingSpeed = 2.0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Homming()
    {
        if (target == null) return;
        Vector3 pos = target.transform.position;
        Vector3 direction = new Vector3(1, 0, 0);
        if (pos.x - transform.position.x > 0) direction = new Vector3(1, 0, 0);
        else direction = new Vector3(-1, 0, 0);

        if (Mathf.Abs(rigidBody.velocity.x) > hommingSpeed) return;
        rigidBody.AddForce(direction * 10, ForceMode.Force);
    }


    void Locking(GameObject target)
    {
        this.target = target;
    }
}
