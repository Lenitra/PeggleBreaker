using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaudron : MonoBehaviour
{
    bool toRight = true;
    public float speed = 0.75f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 1.75f)
        {
            toRight = false;
        }
        if (transform.position.x < -1.75f)
        {
            toRight = true;
        }

        if (toRight)
            transform.position += Vector3.right * Time.deltaTime * speed;
        else
            transform.position += Vector3.left * Time.deltaTime * speed;
        
    }
}
