using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public GameObject spawnpoint_right;
    public GameObject spawnpoint_center;
    public GameObject spawnpoint_left;
    private string name1;
    public bool flag;
    public string current_truck_pos = "center";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        name1 = other.gameObject.name;
        if (name1 == "right")
        {
            current_truck_pos = "right";
            flag = true;
        }
        else if (name1 == "center")
        {
            current_truck_pos = "center";
            flag = true;
        }
        else if (name1 == "left")
        {
            current_truck_pos = "left";
            flag = true;
        }
    }
}
