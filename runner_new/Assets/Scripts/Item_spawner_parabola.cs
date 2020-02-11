using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_spawner_parabola : MonoBehaviour
{
    private static Item_spawner_parabola instance;
    public GameObject[] Itemprephab;
    
    private int random_item;
    public Transform playerTR;
    public Rigidbody rb1;
    //private float force = -200f;
    public GameObject tmp;
    public int check;
    private float speed;
    public GameObject current_item;

    public playerContol PC;
    public Camera_menu_trannsform_contol CC;

    public float spwn_time = 8;

    public SpawnControl SC;
    private Stack<GameObject> monitor = new Stack<GameObject>();
    private Stack<GameObject> pc_block = new Stack<GameObject>();
    private Stack<GameObject> vidik = new Stack<GameObject>();
    public Stack<GameObject> Monitor
    {
        get { return monitor; }
        set { monitor = value; }
    }
    public Stack<GameObject> PC_block
    {
        get { return pc_block; }
        set { pc_block = value; }
    }
    public Stack<GameObject> Vidik
    {
        get { return vidik; }
        set { vidik = value; }
    }

    public static Item_spawner_parabola Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Item_spawner_parabola>();
            }

            return instance;

        }
    }
    
    void Start()
    {
        CreateItems(5);
        StartCoroutine(Wait_time());
        
    }
    private void Update()
    {
        if (PC.canPlay && CC.start_game)
        {
            spwn_time -= .05f * Time.deltaTime;
            spwn_time = Mathf.Clamp(spwn_time, 2, 8);
        }
    }
    public void CreateItems(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            monitor.Push(Instantiate(Itemprephab[0]));
            monitor.Peek().name = "monitor";
            monitor.Peek().SetActive(false);
            pc_block.Push(Instantiate(Itemprephab[1]));
            pc_block.Peek().name = "pc_block";
            pc_block.Peek().SetActive(false);
            vidik.Push(Instantiate(Itemprephab[2]));
            vidik.Peek().name = "vidik";
            vidik.Peek().SetActive(false);
        }
    }
    public void SpawnItems()
    {
        if (PC.speedPU_active || CC.spwn_item || !PC.canPlay)
        {
            tmp.SetActive(false);
        }
        else
        {
            random_item = Random.Range(0, 3);
            switch (random_item)
            {
                case 0:
                    tmp = monitor.Pop();
                    current_item = tmp;

                    break;
                case 1:
                    tmp = pc_block.Pop();
                    current_item = tmp;

                    break;
                case 2:
                    tmp = vidik.Pop();
                    current_item = tmp;

                    break;
            }
            tmp.SetActive(true);


            if (SC.current_truck_pos == "right")
            {
                tmp.transform.position = SC.spawnpoint_right.transform.position;

                check = 0;

            }
            else if (SC.current_truck_pos == "center")
            {
                tmp.transform.position = SC.spawnpoint_center.transform.position;

                check = 1;

            }
            else if (SC.current_truck_pos == "left")
            {
                tmp.transform.position = SC.spawnpoint_left.transform.position;

                check = 2;

            }
            tmp.SetActive(true);
            tmp.transform.parent = PlatfomSpawner.Instance.currentPlatform.transform;
        }
        StartCoroutine(Wait_time());
    }

    IEnumerator Wait_time()
    {
        yield return new WaitForSeconds(spwn_time);
        SpawnItems();
    }
}
