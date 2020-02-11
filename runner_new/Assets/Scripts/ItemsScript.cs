using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsScript : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "item")||(other.tag == "coin"))
        {
            //Item_spawner_parabola.Instance.SpawnItems();
            StartCoroutine(Recycle_item(other));
            //Debug.Log("here");
        }
        if (other.tag == "flying_item")
        {
            StartCoroutine(Recycle_item(other));
            //Debug.Log("here");
        }
    }
    IEnumerator Recycle_item(Collider other)
    {
        yield return new WaitForSeconds(0);
        //Debug.Log("yolo");
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
        else if (other.gameObject.name == "casete_prephab")
        {
            Items_spawner.Instance.Casete_prephab.Push(other.gameObject);
            other.gameObject.SetActive(false);
        }
        
    }
}
