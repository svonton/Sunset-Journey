using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinRotate : MonoBehaviour
{
    float rotatespeed = 50f;
    void Update()
    {
        if (gameObject.name== "joyStick"|| gameObject.name == "Rubic"|| gameObject.name == "tetris") {
            transform.Rotate(Vector3.forward * rotatespeed * Time.deltaTime);
            if (transform.position.y > 0.54f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.53f, transform.position.z), Time.deltaTime * rotatespeed);
            }
            if (transform.position.y < 0.50f) {
                transform.position = new Vector3(transform.position.x, 0.53f, transform.position.z);
            }
        }
        if (gameObject.name == "polaroid")
        {
            transform.Rotate(Vector3.up * rotatespeed * Time.deltaTime);
            if (transform.position.y > 0.54f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.53f, transform.position.z), Time.deltaTime * rotatespeed);
            }
            if (transform.position.y < 0.50f)
            {
                transform.position = new Vector3(transform.position.x, 0.53f, transform.position.z);
            }
        }
        if (gameObject.name == "casete_prephab") {
            transform.Rotate(Vector3.up * rotatespeed * Time.deltaTime);
        }
    }
}
