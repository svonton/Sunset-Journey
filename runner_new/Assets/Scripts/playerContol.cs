using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerContol : MonoBehaviour
{
    public GameObject drone;
    public GameObject zone1;
    public GameObject zone2;
    public Animator Droneanim;
    public swipe swipeContols;
    Rigidbody contoller;
    CapsuleCollider Body;
    public gameManager GM;
    public Animator SkinAnimator;
    public float speed = 5, jumpSpeed = 12;
    int laneNumber = 1,
        laneCount = 2;
    public bool jump = false;
    public bool roll = false;
    public float FirstLanePos, LaneDistance, SideSpeed;
    Vector3 boxCenterNorm = new Vector3(0, 0.16f, 0.7f),
        boxCenterRoll = new Vector3(0.2f, -0.13f, -0.17f);
    float boxHeightNormal = 1.4f,
        boxHeighRoll = 0.7f;
    public bool canPlay;
    public int showAD;
    Vector3 temp;
    public int sign = 0;
    public bool barier_colision = false;

    public float fall = 4;

    public bool speedPU_active = false;

    public bool deth_fix = true;

    public float roll_me = 0.0f;

    public bool drone_buff, multy_buff;
    public int buff_number = 0;

    // Start is called before the first frame update
    void Start()
    {
        contoller = GetComponent<Rigidbody>();
        Body = GetComponent<CapsuleCollider>();
        deth_fix = true;
    }

    private void FixedUpdate()
    {

        contoller.AddForce(new Vector3(0, Physics.gravity.y * 3f, 0), ForceMode.Acceleration);
        if (jump && isGrounded())
        {
            contoller.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            SkinAnimator.SetTrigger("jump_up");
            Body.center = boxCenterNorm;
            Body.height = boxHeightNormal;
            jump = false;
        }
    }


    void Update()
    {
        if (isGrounded() && canPlay)
        {
            if (swipeContols.SwipeUp)
            {
                jump = true;
            }
        }
        if ((transform.position.z < -1.163f) && (canPlay))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.163f);
        }
        if (roll_me < 1)
        {
            roll_me += 0.01f;
        }
        if (roll_me >= 1)
        {
            Body.center = boxCenterNorm;
            Body.height = boxHeightNormal;
        }
        sign = 0;
        if (canPlay)
        {
            if (swipeContols.SwipeDown)
            {
                contoller.AddForce(new Vector3(0, Physics.gravity.y * 10f, 0), ForceMode.Acceleration);
                Body.center = boxCenterRoll;
                Body.height = boxHeighRoll;
                roll_me = 0;
                SkinAnimator.SetTrigger("roll");
            }
            if (swipeContols.SwipeLeft)
            {
                sign = -1;

            }
            else if (swipeContols.SwipeRight)
            {
                sign = 1;
            }
        }
        laneNumber += sign;
        laneNumber = Mathf.Clamp(laneNumber, 0, laneCount);
        if (barier_colision)
        {
            laneNumber = 1;
            barier_colision = false;
        }
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, FirstLanePos + (laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "casete_prephab")
        {
            GM.AddCoins(1);
            AudioManager.Insctance.coins_SE();
            Items_spawner.Instance.Casete_prephab.Push(other.gameObject);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.name == "Rubic")
        {
            other.gameObject.SetActive(false);
            AudioManager.Insctance.buff_pickup_SE();
            StartCoroutine(dronePU());
        }
        if (other.gameObject.name == "joyStick")
        {
            other.gameObject.SetActive(false);
            AudioManager.Insctance.buff_pickup_SE();
            StartCoroutine(speedPU());
        }
        if (other.gameObject.name == "tetris")
        {
            other.gameObject.SetActive(false);
            AudioManager.Insctance.buff_pickup_SE();
            GM.AddRewindBuff(1);
        }
        if (other.gameObject.name == "polaroid")
        {
            other.gameObject.SetActive(false);
            AudioManager.Insctance.buff_pickup_SE();
            StartCoroutine(multiplierPU());

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!speedPU_active)
        {
            if (collision.gameObject.CompareTag("item") || collision.gameObject.CompareTag("flying_item") || collision.gameObject.CompareTag("barier"))
            {
                if (deth_fix)
                {
                    GM.ShowResult();
                    deth_fix = false;
                    showAD++;
                    if (showAD >= 5)
                    {
                        showAD = 0;
                        GM.Request_On_Death();
                    }
                }
                canPlay = false;
            }
        }
        if ((collision.gameObject.name == "futuristicSample_road_barier") || (collision.gameObject.name == "futuristicSample_road_barier_1"))
        {
            barier_colision = true;
        }
        //if (collision.gameObject.name == "Rubic") {
        //    collision.gameObject.SetActive(false);
        //    AudioManager.Insctance.buff_pickup_SE();
        //    StartCoroutine(dronePU());
        //}
        //if (collision.gameObject.name == "joyStick")
        //{
        //    collision.gameObject.SetActive(false);
        //    AudioManager.Insctance.buff_pickup_SE();
        //    StartCoroutine(speedPU());

        //}
        //if (collision.gameObject.name == "casete_prephab") {
        //    GM.AddCoins(1);
        //    AudioManager.Insctance.coins_SE();
        //    Items_spawner.Instance.Casete_prephab.Push(collision.gameObject);
        //    collision.gameObject.SetActive(false);
        //}
        //if (collision.gameObject.name == "tetris")
        //{
        //    collision.gameObject.SetActive(false);
        //    AudioManager.Insctance.buff_pickup_SE();
        //    GM.AddRewindBuff(1);
        //}
        //if (collision.gameObject.name == "polaroid")
        //{
        //    collision.gameObject.SetActive(false);
        //    AudioManager.Insctance.buff_pickup_SE();
        //    StartCoroutine(multiplierPU());

        //}
    }
    IEnumerator speedPU()
    {
        speedPU_active = true;
        buff_number = 1;
        GM.buff_name_func();
        GM.takeTimeSpeed = GM.platform_speed;
        yield return new WaitForSeconds(10f);
        speedPU_active = false;
        GM.buff_name_func();
    }
    IEnumerator dronePU()
    {
        drone_buff = true;
        buff_number = 3;
        GM.buff_name_func();
        drone.SetActive(true);
        zone1.SetActive(true);
        zone2.SetActive(true);
        Droneanim.SetTrigger("arriving");
        yield return new WaitForSeconds(10);
        Droneanim.SetTrigger("leaving");
        yield return new WaitForSeconds(3f);
        zone1.SetActive(false);
        zone2.SetActive(false);
        drone_buff = false;
        drone.SetActive(false);
        GM.buff_name_func();
    }
    IEnumerator multiplierPU()
    {
        multy_buff = true;
        buff_number = 2;
        GM.buff_name_func();
        GM.ScoreMultiplier = 5;
        yield return new WaitForSeconds(10f);
        GM.ScoreMultiplier = 1;
        multy_buff = false;
        GM.buff_name_func();
    }

}
