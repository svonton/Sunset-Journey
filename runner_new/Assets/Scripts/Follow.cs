using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    Vector3 lookdirection;
    public float speed = 12.0f;
    const float epsilon = 0.5f;

    void Update()
    {
        lookdirection = (target.position - transform.position).normalized;
        if ((transform.position - target.position).magnitude > epsilon)
            transform.Translate(lookdirection * Time.deltaTime * speed);
        if (transform.position.y<7.78f) {
            transform.position = new Vector3(transform.position.x, 7.78f, transform.position.z);
        }
    }
}
