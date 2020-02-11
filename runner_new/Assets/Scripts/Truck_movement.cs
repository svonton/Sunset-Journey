using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_movement : MonoBehaviour
{
    public Animator anim;
    private int randomtime = 4;
    private int time = 0;
    private int randtime_roadwork;
    private string side;
    private string lamp_side;
    //private bool flag = true;
    public Camera_menu_trannsform_contol CM;
    //private int randomanim_right;
    //private int randomanim_left;
    //private int randomanim_center;
    private int randomanim;
    public string right_or_left_or_center = "center";
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        StartCoroutine(Delay2());
    }

    IEnumerator Delay2()
    {
        if (CM.start_truck)
        {
            yield return new WaitForSeconds(time);
            yield return new WaitForSeconds(randomtime);
            randomtime = Random.Range(4, 7);
            randomanim = Random.Range(1, 3);
            if (right_or_left_or_center == "right")
            {
                if (side == "left")
                {
                    randomanim = 2;
                }
                if (side == "right")
                {
                    randomanim = Random.Range(1, 3);
                }
                if (lamp_side == "left")
                {
                    randomanim = 0;
                    StartCoroutine(Delay2());
                }
                if (lamp_side == "right")
                {
                    randomanim = 1;
                }
                anim.SetBool("isCenter_to_right", false);
                anim.SetBool("isLeft_to_right", false);
                anim.SetBool("isCenter_to_left", false);
                anim.SetBool("isRight_to_left", false);
                anim.SetBool("isRight_to_center", false);
                anim.SetBool("isLeft_to_center", false);
                if (randomanim == 1)
                {
                    anim.SetBool("isRight_to_left", true);
                    right_or_left_or_center = "left";
                    StartCoroutine(Delay2());
                    //Debug.Log("left");
                }
                if (randomanim == 2)
                {
                    anim.SetBool("isRight_to_center", true);
                    right_or_left_or_center = "center";
                    StartCoroutine(Delay2());
                    //Debug.Log("center");
                }
            }
            else if (right_or_left_or_center == "left")
            {
                if (side == "left")
                {
                    randomanim = Random.Range(1, 3);
                }
                if (side == "right")
                {
                    randomanim = 2;
                }
                if (lamp_side == "left")
                {
                    randomanim = 1;
                }
                if (lamp_side == "right")
                {
                    randomanim = 0;
                    StartCoroutine(Delay2());
                }

                anim.SetBool("isCenter_to_right", false);
                anim.SetBool("isLeft_to_right", false);
                anim.SetBool("isCenter_to_left", false);
                anim.SetBool("isRight_to_left", false);
                anim.SetBool("isRight_to_center", false);
                anim.SetBool("isLeft_to_center", false);
                if (randomanim == 1)
                {
                    anim.SetBool("isLeft_to_right", true);
                    right_or_left_or_center = "right";
                    StartCoroutine(Delay2());
                    //Debug.Log("right");
                }
                if (randomanim == 2)
                {
                    anim.SetBool("isLeft_to_center", true);
                    right_or_left_or_center = "center";
                    StartCoroutine(Delay2());
                    // Debug.Log("center");
                }
            }
            else if (right_or_left_or_center == "center")
            {

                if (side == "left")
                {
                    randomanim = 1;
                }
                if (side == "right")
                {
                    randomanim = 2;
                }
                if (lamp_side == "left")
                {
                    randomanim = 1;
                }
                if (lamp_side == "right")
                {
                    randomanim = 2;
                }
                anim.SetBool("isCenter_to_right", false);
                anim.SetBool("isLeft_to_right", false);
                anim.SetBool("isCenter_to_left", false);
                anim.SetBool("isRight_to_left", false);
                anim.SetBool("isRight_to_center", false);
                anim.SetBool("isLeft_to_center", false);

                if (randomanim == 1)
                {
                    anim.SetBool("isCenter_to_right", true);
                    right_or_left_or_center = "right";
                    StartCoroutine(Delay2());
                    //Debug.Log("right");
                }
                if (randomanim == 2)
                {
                    anim.SetBool("isCenter_to_left", true);
                    right_or_left_or_center = "left";
                    StartCoroutine(Delay2());
                    //Debug.Log("left");
                }
            }
        }
        else {
            yield return new WaitForSeconds(1);
            StartCoroutine(Delay2());
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        randomtime = 2;
        yield return new WaitForSeconds(5);
        side = " ";
    }
    IEnumerator delay2()
    {
        yield return new WaitForSeconds(1);
        randomtime = 4;
        lamp_side = " ";
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "neon_road_sign_left")
        {
            StopAllCoroutines();
            side = "left";
            randomtime = 0;
            StartCoroutine(Delay2());
            StartCoroutine(delay());
        }

        else if (other.name == "neon_road_sign_right")
        {
            StopAllCoroutines();
            side = "right";
            randomtime = 0;
            StartCoroutine(Delay2());
            StartCoroutine(delay());
        }
        else if (other.name == "broken_lamp_right")
        {
            StopAllCoroutines();
            lamp_side = "right";
            randomtime = 0;
            StartCoroutine(Delay2());
            StartCoroutine(delay2());
        }
        else if (other.name == "broken_lamp_left")
        {
            StopAllCoroutines();
            lamp_side = "left";
            randomtime = 0;
            StartCoroutine(Delay2());
            StartCoroutine(delay2());
        }
    }

}
