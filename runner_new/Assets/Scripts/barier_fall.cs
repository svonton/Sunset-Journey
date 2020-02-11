using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barier_fall : MonoBehaviour
{
    public Animator barier_fd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "truck") {
            StartCoroutine(time_before_fall());
        }
    }
    IEnumerator time_before_fall()
    {
        yield return new WaitForSeconds(0.5f);
        barier_fd.SetTrigger("barier_fall");
    }
}
