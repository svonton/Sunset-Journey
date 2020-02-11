using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipecontrol : MonoBehaviour
{
    public swipe swipeContols;
    public Transform player;
    private Vector3 desiredPosition;

    // Update is called once per frame

    private void Start()
    {
        
        //desiredPosition = player.position;
    }
    void Update()
    {
       
        if (swipeContols.SwipeLeft)
            desiredPosition += new Vector3(-0.7f,0,0);
        if (swipeContols.SwipeRight)
            desiredPosition += new Vector3(0.7f, 0, 0);
        if (swipeContols.SwipeUp)
            desiredPosition += Vector3.up;
        if (swipeContols.SwipeDown)
            desiredPosition += Vector3.down;
        
        player.transform.position = Vector3.MoveTowards(player.transform.position, desiredPosition, 3f * Time.deltaTime);
        if (swipeContols.Tap)
            Debug.Log("tap");
    }
}
