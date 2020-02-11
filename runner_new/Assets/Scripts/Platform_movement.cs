using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_movement : MonoBehaviour
{
    Vector3 moveVec;
    public gameManager GM;
    public playerContol PC;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<gameManager>();
        PC = FindObjectOfType<playerContol>();
        moveVec = new Vector3(0, 0, -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PC.canPlay) {
            transform.Translate(moveVec * Time.deltaTime * GM.platform_speed);
        }
    }
}
