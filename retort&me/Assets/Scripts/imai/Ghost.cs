using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{

    [SerializeField]
    private float turnTime = 6.0f;

    [SerializeField]
    private float hommingSpeed = 2.0f;

    [SerializeField]
    bool homming = false;

    Vector2 targetPos;

    Rigidbody rigidBody = null;

    IEnumerator turnIterator;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        turnIterator = TurnIterator();
        StartCoroutine(turnIterator);
    }

    public void InSight(Vector2 targetPos_)
    {
        targetPos = targetPos_;
        homming = true;
        StopCoroutine(turnIterator);
    }

    public void Missing()
    {
        homming = false;
        StartCoroutine(turnIterator);
    }

    IEnumerator TurnIterator()
    {
        while (true)
        {
            yield return new WaitForSeconds(turnTime);
            transform.Rotate(0,180,0);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        //本来ここはプレイヤーのdeady呼ぶべき
        if(coll.gameObject.tag == "Player")
        {
            GameManager.Instace.Dead();
        }
    }

    void Homming()
    {
        if (!homming) return;
        Vector3 direction = new Vector3(1, 0, 0);
        if (targetPos.x - transform.position.x > 0) direction = new Vector3(1, 0, 0);
        else direction = new Vector3(-1, 0, 0);

        if (Mathf.Abs(rigidBody.velocity.x) > hommingSpeed) return;
        rigidBody.AddForce(direction * 10, ForceMode.Force);
    }

    void FixedUpdate()
    {
        Homming();
    }
}
