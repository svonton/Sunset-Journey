using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_idle_state : MonoBehaviour
{
    Transform tr;
    Vector3 pos;
    float x = 2.469262f;
    float speed = 0.002f;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = new Vector3(1.643565f, x + speed, -0.1162925f);
        transform.position = pos;
        if (x > 2.545)
            speed = -0.002f;
        else if (x < 2.469262)
            speed = 0.002f;
    }
}
