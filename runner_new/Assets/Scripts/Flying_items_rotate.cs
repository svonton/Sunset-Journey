using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_items_rotate : MonoBehaviour
{
    public bool spin=true;
    public int rotatespeed = 50;

    void Update()
    {
        if (transform.position.y>2) {
            spin = true;
        }
        if (spin) {
            transform.Rotate(Vector3.up * rotatespeed * Time.deltaTime);
            transform.Rotate(Vector3.right * rotatespeed * Time.deltaTime);
            transform.Rotate(Vector3.forward * rotatespeed * Time.deltaTime);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        spin = false;
    }
}
