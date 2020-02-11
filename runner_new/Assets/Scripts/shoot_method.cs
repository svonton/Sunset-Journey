using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_method : MonoBehaviour
{
    public GameObject bullet, drone;
    Vector3 posit;
    Vector3 drone_pos;
    public float speed = 2.5f;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.name != "Sphere") && (other.name != "Sphere_help")&&(other.name != "casete_prephab"))
        {
            bullet.SetActive(true);
            posit = other.GetComponent<Transform>().transform.position;
            drone_pos = drone.transform.position;
            bullet.transform.position = drone_pos;
        }
    }
    private void Update()
    {
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, posit, Time.deltaTime * speed);
    }
}
