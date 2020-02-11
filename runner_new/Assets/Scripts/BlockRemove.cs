using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRemove : MonoBehaviour
{
    public int count = 1;
    public int distance = 0;
    public bool spwnTime = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cube")
        {
            if (other.gameObject.name == "Cube.013")
            {
                other.GetComponent<Animator>().SetTrigger("spawn_idle");
            }
            other.gameObject.SetActive(false);
        }
        if (other.tag == "side_platform") {
            other.gameObject.SetActive(false);
        }
        if (other.tag == "coin")
        {
            Items_spawner.Instance.Casete_prephab.Push(other.gameObject);
            other.gameObject.SetActive(false);
        }
        if (other.tag == "platform") {
            count++;
            distance++;
            if (distance == 6)
            {
                spwnTime = true;
                distance = 0;
            }
            else {
                spwnTime = false;
            }
        }
    }
}
