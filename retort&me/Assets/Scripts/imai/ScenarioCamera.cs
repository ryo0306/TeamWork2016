using UnityEngine;
using System.Collections;


//itweenでうまくできるか検証
public class ScenarioCamera : MonoBehaviour {

    [SerializeField]
    Vector3[] tagetPos;

    private int targetNum = 0;

    private float time = 0;

    [SerializeField]
    private float speed = 0.001f;


    void Move()
    {

    }
}
