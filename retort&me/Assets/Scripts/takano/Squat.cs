using UnityEngine;
using System.Collections;

public class Squat : MonoBehaviour {

    SpriteRenderer renderer;
    public Sprite[] image;
    int content;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Squatt()
    {

      
           
            if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(content);
            renderer.sprite = image[content];
            content++;
                if (content >= image.Length) content = 0;
            }

        
    }

            // Update is called once per frame
            void Update () {
        Squatt();
            }
        }
    