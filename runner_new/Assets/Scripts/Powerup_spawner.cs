using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_spawner : MonoBehaviour
{

    public GameObject[] powerups;
    private GameObject tmp;
    int rand;
    bool fix = true;
    private Rigidbody rb;
    private BoxCollider box;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "TRY_THIS_drone sample_colored")
        {
            StartCoroutine(doublespwn_fix());
        }
    }
    IEnumerator doublespwn_fix()
    {
        if (fix)
        {
            fix = false;
            for (int i = 0; i < powerups.Length; i++)
            {
                powerups[i].SetActive(false);
            }
            rand = Random.Range(0, 4);
            powerups[rand].transform.parent = null;
            powerups[rand].transform.position = transform.position;
            powerups[rand].SetActive(true);
            powerups[rand].transform.parent = PlatfomSpawner.Instance.currentPlatform.transform;
            rb = powerups[rand].GetComponent<Rigidbody>();
            box = powerups[rand].GetComponent<BoxCollider>();
            rb.isKinematic = false;
            box.isTrigger = false;
            StartCoroutine(wait());
        }
        yield return new WaitForSeconds(3f);
        fix = true;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = true;
        box.isTrigger = true;
    }
}
