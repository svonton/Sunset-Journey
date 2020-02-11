using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icon_blink : MonoBehaviour
{
    public GameObject icon,x5;
    public playerContol PC;
    public gameManager GM;
    GameObject tmr;
    float time_blink = 0.5f;
    bool st_cor = true;
    void Update()
    {
        if (PC.speedPU_active&&st_cor) {
            tmr = icon;
            time_blink = 0.5f;
            StartCoroutine(ic_blink());
        }
        if (GM.rewind_active && st_cor)
        {
            tmr = GM.rew_icon;
            time_blink = 0.25f;
            StartCoroutine(ic_blink());
        }
        if (PC.multy_buff && st_cor)
        {
            tmr = x5;
            time_blink = 0.5f;
            StartCoroutine(ic_blink());
        }
    }
    IEnumerator ic_blink() {
        yield return new WaitForSeconds(time_blink);
        tmr.SetActive(true);
        st_cor = false;
        yield return new WaitForSeconds(time_blink);
        tmr.SetActive(false);
        st_cor = true;
    }
}
