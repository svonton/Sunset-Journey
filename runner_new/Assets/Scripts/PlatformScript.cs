using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public playerContol PC;
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PlatfomSpawner.Instance.SpawnPlatform();
            StartCoroutine(Recycle());
        }
        if (other.tag == "plate_fix") {
            PlatfomSpawner.Instance.Platforms.Push(gameObject);
            gameObject.SetActive(false);
        }
    }

    IEnumerator Recycle()
    {
        if(gameObject.name == "main_road")
        {
            yield return new WaitForSeconds(2);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            
            gameObject.transform.GetChild(10).gameObject.SetActive(false);
            
            gameObject.transform.GetChild(11).gameObject.SetActive(false);

            gameObject.transform.GetChild(14).gameObject.SetActive(false);
            gameObject.transform.GetChild(15).gameObject.SetActive(false);
            if (!PC.canPlay)
            {
                StopCoroutine(Recycle());
            }
            else {
                PlatfomSpawner.Instance.Platforms.Push(gameObject);
                gameObject.SetActive(false);
            }
            
        }

    }
}
