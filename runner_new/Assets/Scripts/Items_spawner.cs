using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_spawner : MonoBehaviour
{
    private static Items_spawner instance;
    public GameObject[] Itemprephab;

    public float spwn_time = 4;

    public playerContol PC;

    private int random_item;
    private int random_rotation;
    public Transform playerTR;
    public Rigidbody rb;
    private BoxCollider casete_box_col;
    private Vector3 goriz;
    //private float force = 100;
    public GameObject tmp;
    public int check;
    private float speed;
    public GameObject current_item;
    public SpawnControl SC;
    private Stack<GameObject> arcanoid = new Stack<GameObject>();
    private Stack<GameObject> server_stand = new Stack<GameObject>();
    private Stack<GameObject> casete_prephab = new Stack<GameObject>();
    public Camera_menu_trannsform_contol CC;
    public Stack<GameObject> Arcanoid
    {
        get { return arcanoid; }
        set { arcanoid = value; }
    }
    public Stack<GameObject> Server_stand
    {
        get { return server_stand; }
        set { server_stand = value; }
    }
    public Stack<GameObject> Casete_prephab
    {
        get { return casete_prephab; }
        set { casete_prephab = value; }
    }

    public static Items_spawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Items_spawner>();
            }

            return instance;

        }
    }
    private void Update()
    {
        if (PC.canPlay && CC.start_game)
        {
            spwn_time -= .025f * Time.deltaTime;
            spwn_time = Mathf.Clamp(spwn_time, 1, 4);
        }
    }

    void Start()
    {
        CreateItems(5);
        StartCoroutine(Wait_time());

    }

    public void CreateItems(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            arcanoid.Push(Instantiate(Itemprephab[0]));
            arcanoid.Peek().name = "arcanoid";
            arcanoid.Peek().SetActive(false);
            server_stand.Push(Instantiate(Itemprephab[1]));
            server_stand.Peek().name = "server_stand";
            server_stand.Peek().SetActive(false);
            casete_prephab.Push(Instantiate(Itemprephab[2]));
            casete_prephab.Peek().name = "casete_prephab";
            casete_prephab.Peek().SetActive(false);
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
                    tmp = arcanoid.Pop();
                    break;
                case 1:
                    tmp = server_stand.Pop();
                    break;
                case 2:
                    tmp = casete_prephab.Pop();
                    rb = tmp.GetComponent<Rigidbody>();
                    casete_box_col = tmp.GetComponent<BoxCollider>();
                    rb.isKinematic = false;
                    casete_box_col.isTrigger = false;
                    StartCoroutine(wait());
                    break;
            }

            random_rotation = Random.Range(0, 3);
            if (SC.current_truck_pos == "right")
            {
                tmp.transform.position = SC.spawnpoint_right.transform.position;
                if (random_item != 2)
                {
                    switch (random_rotation)
                    {
                        case 0:
                            tmp.transform.rotation = Quaternion.Euler(0, 90, 0);

                            break;
                        case 1:
                            tmp.transform.rotation = Quaternion.Euler(0, 180, 0);

                            break;
                        case 2:
                            tmp.transform.rotation = Quaternion.Euler(-90, 180, 0);

                            break;
                    }
                }

                check = 0;

            }
            else if (SC.current_truck_pos == "center")
            {
                tmp.transform.position = SC.spawnpoint_center.transform.position;
                if (random_item != 2)
                {
                    switch (random_rotation)
                    {
                        case 0:
                            tmp.transform.rotation = Quaternion.Euler(0, 90, 0);
                            break;
                        case 1:
                            tmp.transform.rotation = Quaternion.Euler(0, 180, 0);
                            break;
                        case 2:
                            tmp.transform.rotation = Quaternion.Euler(-90, 180, 0);
                            break;
                    }
                }

                check = 1;

            }
            else if (SC.current_truck_pos == "left")
            {
                tmp.transform.position = SC.spawnpoint_left.transform.position;
                if (random_item != 2)
                {
                    switch (random_rotation)
                    {
                        case 0:
                            tmp.transform.rotation = Quaternion.Euler(0, 90, 0);

                            break;
                        case 1:
                            tmp.transform.rotation = Quaternion.Euler(0, 180, 0);
                            break;
                        case 2:

                            break;
                    }
                }

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

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = true;
        casete_box_col.isTrigger = true;
    }
}
