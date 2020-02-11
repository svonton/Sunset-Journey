using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfomSpawner : MonoBehaviour
{
    public GameObject platformPrephab;
    public GameObject currentPlatform;
    public bool spawnpl = true;
    public BlockRemove BR;
    private int rand;
    private int rnd;
    int rand1 = 0;
    private int bar_rand;
    private int road_work_rand;
    public int randbr_lamp; // make private
    int r = 0;
    int r1 = 0;
    int randside;
    int randside1;
    public bool right = true;
    public int k = 0;
    public int br_lamp = 0;
    private string barierside = "nothing";
    public bool spawnonnextplatf;
    private int canspawn;
    private bool before_wr = true;
    private bool after_wr = true;
    public playerContol PC;
    public int mark = 0;

    
    // public int randomnumber;

    // private int pos1;
    // private int pos2;

    // private int type1;
    // private int type2;



    private static PlatfomSpawner instance;

    private Stack<GameObject> platforms = new Stack<GameObject>();

    public Stack<GameObject> Platforms
    {
        get { return platforms; }
        set { platforms = value; }
    }

    public static PlatfomSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlatfomSpawner>();
            }

         return instance;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePlatforms(15);
        bar_rand = Random.Range(4, 11); //first_rand_bar
        road_work_rand = Random.Range(15, 21); // first road_work
        randbr_lamp = Random.Range(10, 17); // first broken lamp
        for (int i = 0; i < 13; i++)
        {
            SpawnPlatform();
        } 
    }

    public void CreatePlatforms(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            platforms.Push(Instantiate(platformPrephab));
            platforms.Peek().name = "main_road";
            platforms.Peek().SetActive(false);

        }
    }

    public void SpawnPlatform()
    {
        currentPlatform.transform.GetChild(16).gameObject.SetActive(true);
        currentPlatform.transform.GetChild(17).gameObject.SetActive(true);
        currentPlatform.transform.GetChild(18).gameObject.SetActive(true);
        currentPlatform.transform.GetChild(19).gameObject.SetActive(true);
        currentPlatform.transform.GetChild(20).gameObject.SetActive(true);
        currentPlatform.transform.GetChild(21).gameObject.SetActive(true);
        if (platforms.Count == 0)
        {
            CreatePlatforms(10);
        }
        GameObject tmp = platforms.Pop();
        tmp.SetActive(true);
        tmp.transform.position = currentPlatform.transform.GetChild(0).transform.GetChild(0).position;
        currentPlatform = tmp;

        Bariers();
        Bottom_planes();
        Lamp();
    }
    public void Bottom_planes()
    {
        rand = Random.Range(0, 3);
        while (rand == rand1)
        {
            rand = Random.Range(0, 3);
        }
        rand1 = rand;
        switch (rand)
        {
            case 0:
                currentPlatform.transform.GetChild(6).gameObject.SetActive(true);
                currentPlatform.transform.GetChild(7).gameObject.SetActive(false);
                currentPlatform.transform.GetChild(8).gameObject.SetActive(false);
                break;
            case 1:
                currentPlatform.transform.GetChild(7).gameObject.SetActive(true);
                currentPlatform.transform.GetChild(6).gameObject.SetActive(false);
                currentPlatform.transform.GetChild(8).gameObject.SetActive(false);
                break;
            case 2:
                currentPlatform.transform.GetChild(8).gameObject.SetActive(true);
                currentPlatform.transform.GetChild(7).gameObject.SetActive(false);
                currentPlatform.transform.GetChild(6).gameObject.SetActive(false);
                break;
        }
    }

    public void Bariers()
    {
        if (mark == 2)
        {
            mark = 0;
        }
        r++;
        r1++;
        if(r1 > 2)
        {
            after_wr = true;
        }
        
        if (r1 >= road_work_rand && before_wr == true && !PC.speedPU_active)
        {
            
            road_work_rand = Random.Range(15, 21);
            randside1 = Random.Range(0, 2);
            switch(randside1)
            {
                case 0:
                    currentPlatform.transform.GetChild(12).GetChild(0).gameObject.SetActive(true);
                    currentPlatform.transform.GetChild(12).GetChild(1).gameObject.SetActive(true);
                    currentPlatform.transform.GetChild(12).GetChild(2).gameObject.SetActive(true);
                    barierside = "right";
                    break;
                case 1:
                    currentPlatform.transform.GetChild(13).GetChild(0).gameObject.SetActive(true);
                    currentPlatform.transform.GetChild(13).GetChild(1).gameObject.SetActive(true);
                    currentPlatform.transform.GetChild(13).GetChild(2).gameObject.SetActive(true);
                    barierside = "left";
                    break;
            }
            spawnonnextplatf = true;
            r1 = 0;
            mark ++;
        }
        else if (spawnonnextplatf)
        {
            switch(randside1)
            {
                case 0:
                    currentPlatform.transform.GetChild(12).GetChild(1).gameObject.SetActive(true);
                    currentPlatform.transform.GetChild(12).GetChild(2).gameObject.SetActive(true);
                    currentPlatform.transform.GetChild(12).GetChild(3).gameObject.SetActive(true);
                    barierside = "right";
                    break;
                case 1:
                    currentPlatform.transform.GetChild(13).GetChild(1).gameObject.SetActive(true);
                    currentPlatform.transform.GetChild(13).GetChild(2).gameObject.SetActive(true);
                    currentPlatform.transform.GetChild(13).GetChild(3).gameObject.SetActive(true);
                    barierside = "left";
                    break;
            }
            spawnonnextplatf = false;
            mark++;
            after_wr = false;
        }

        
        if (r >= bar_rand && !PC.speedPU_active) {
            bar_rand = Random.Range(6, 13);
            if(barierside == "nothing")
            randside = Random.Range(0, 3);
            else if (barierside == "right")
                randside = 0;
            else if (barierside == "left")
                randside = 1;
            switch (randside)
            {
                case 0: currentPlatform.transform.GetChild(10).gameObject.SetActive(true);
                    break;
                case 1: currentPlatform.transform.GetChild(11).gameObject.SetActive(true);
                    break;
                case 2: currentPlatform.transform.GetChild(10).gameObject.SetActive(true);
                        currentPlatform.transform.GetChild(11).gameObject.SetActive(true);
                    break;
            }
            r = 0;
            barierside = "nothing";
        }
    }

    public void Lamp()
    {

        k++;
        br_lamp++;
        if (k == 2)
        {
            
            if (right)
            {
                if (br_lamp > randbr_lamp) {
                    br_lamp = 0;
                    randbr_lamp = Random.Range(10, 17);
                    if ((mark != 1) && (mark != 2) && (after_wr == true))
                    {
                        if (!PC.speedPU_active)
                        {
                            currentPlatform.transform.GetChild(14).gameObject.SetActive(true);
                            before_wr = false;
                        }
                    }
                    else
                    {
                        currentPlatform.transform.GetChild(3).gameObject.SetActive(true);//lamp left
                        before_wr = true;
                    }
                }
                else {
                    currentPlatform.transform.GetChild(3).gameObject.SetActive(true);//lamp left
                    before_wr = true;
                }
                right = false;
            }
            else
            {
                if (br_lamp > randbr_lamp)
                {
                    br_lamp = 0;
                    randbr_lamp = Random.Range(10, 17);
                    if ((mark != 1) && (mark != 2) && (after_wr == true))
                    {
                        if (!PC.speedPU_active)
                        {
                            currentPlatform.transform.GetChild(15).gameObject.SetActive(true);
                            before_wr = false;
                        }
                    }
                    else
                    {
                        currentPlatform.transform.GetChild(4).gameObject.SetActive(true);//lamp right
                        before_wr = true;
                    }
                    
                }
                else
                {
                    currentPlatform.transform.GetChild(4).gameObject.SetActive(true);//lamp right
                    before_wr = true;
                }
                right = true;
            }
        }
        if (k > 2)
            k = 0;
    }
}
