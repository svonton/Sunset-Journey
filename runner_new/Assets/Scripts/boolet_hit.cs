using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boolet_hit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag=="item")||(other.tag == "flying_item")) {
            if (other.gameObject.name == "monitor")
            {
                Item_spawner_parabola.Instance.Monitor.Push(other.gameObject);
                other.gameObject.SetActive(false);
                //Debug.Log("monitor");
            }
            else if (other.gameObject.name == "arcanoid")
            {
                Items_spawner.Instance.Arcanoid.Push(other.gameObject);
                other.gameObject.SetActive(false);
                //Debug.Log("arcanoid");
            }
            else if (other.gameObject.name == "pc_block")
            {
                Item_spawner_parabola.Instance.PC_block.Push(other.gameObject);
                other.gameObject.SetActive(false);
                // Debug.Log("pc");
            }
            else if (other.gameObject.name == "server_stand")
            {
                Items_spawner.Instance.Server_stand.Push(other.gameObject);
                other.gameObject.SetActive(false);
                //Debug.Log("serverd");
            }
            else if (other.gameObject.name == "vidik")
            {
                Item_spawner_parabola.Instance.Vidik.Push(other.gameObject);
                other.gameObject.SetActive(false);
                //Debug.Log("vidik");
            }
            //gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        StartCoroutine(lifeTime());
    }
    IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
}



