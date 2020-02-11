using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delorean_control : MonoBehaviour
{
    public Animator anim;
    public int starttime = 15;
    public int side;
    public GameObject drones;
    public GameObject delor;
    public Follow F;

    public playerContol PC;
    public Camera_menu_trannsform_contol CC;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn_time());
    }

    IEnumerator Spawn_time()
    {
        F.speed = 12f;
        drones.transform.position = new Vector3(-0.07933675f, 7.782822f, -18.66824f);
        delor.SetActive(false);
        drones.SetActive(false);
        yield return new WaitForSeconds(starttime);
        starttime = Random.Range(15, 21);
        side = Random.Range(0, 3);
        
        if (PC.speedPU_active||CC.spwn_item|| !PC.canPlay)
        {
            delor.SetActive(false);
            drones.SetActive(false);
        }
        else
        {
            delor.SetActive(true);
            drones.SetActive(true);
        }
        switch (side)
        {
            case 0:
                anim.SetTrigger("right");
                break;
            case 1:
                anim.SetTrigger("left");
                F.speed = 18f;
                break;
            case 2:
                anim.SetTrigger("center");
                break;
        }
        if (delor.active) {
            AudioManager.Insctance.delor_SE();
        }
        yield return new WaitForSeconds(8);
        StartCoroutine(Spawn_time());
    }
}
