using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;

public class gameManager : MonoBehaviour
{
    public GameObject Result;
    public playerContol PM;
    public BlockRemove BR;
    public PlatformScript RS;
    //public Text scoreTxt,coinsTxt;
    public TextMeshProUGUI scoreTxt, coinsTxt;
    float points;
    public int coins = 0;
    public int run_coins = 0;
    public float platform_speed = 3;
    float fieldofview = 65f;
    public GameObject sky_star;
    public GameObject leftDestr;
    public GameObject rightDestr;
    Camera_menu_trannsform_contol CC;
    float maxSpeed = 20;
    public float takeTimeSpeed;

    string score_format;
    int format_for_score = 1000000;
    int result_score = 0;
    string st_result_score;
    //
    public TextMeshProUGUI doble_coins_text;
    public Button doble_coins_btn;
    public Button buf_add_button;
    public Button rew_btn;
    public bool death_when_add;
    //
    private float music_point;
    public string coins_format = "00:00";
    int format_for_coins = 10000;
    int result_coins = 0;
    string st_result_coins;

    public int rewind_buff_count = 0;
    int rewind_buff_cost = 1;
    public TextMeshProUGUI rew_buff_txt, result_rew_buff_txt, rew_cost_txt, rewind_btn;
    public GameObject rew_anim;
    public GameObject rew_icon;
    public bool rewind_active = false;


    public GameObject play_txt;
    public GameObject distance;
    public GameObject coins_ui;
    public GameObject stop;
    public GameObject stop_ic;
    bool result_bool = true;
    public TextMeshProUGUI deth_dis, deth_coins, deth_double_up_txt, deth_coins_offer;
    public Animator TR;

    public int ScoreMultiplier = 1;
    bool deth_font_size = true;
    bool deth_font_size_anim = false;


    public int[] stats = { 8, 7, 6, 5, 4, 3, 2, 1, 0 };

    public List<Skin> Skins;

    public string buff_name = "sample";
    string current_text = "";
    public TextMeshProUGUI buff_name_txt;
    public GameObject buff_name_plate, buff_name_obj;

    private void Start()
    {
        if (stats.Length == 0)
        {
            stats = new int[9];
        }
        Camera.main.fieldOfView = fieldofview;
        CC = GetComponent<Camera_menu_trannsform_contol>();
        rew_buff_txt.text = rewind_buff_count.ToString();

    }
    private void Awake()
    {
        //Application.targetFrameRate = 60;
        Advertisement.Initialize("3340403");
    }
    public void StarGame()
    {
        Result.SetActive(false);
        deth_font_size_anim = false;
        PM.canPlay = true;
        //RS.StartGame();//
        points = 0;
        result_bool = true;
        Application.LoadLevel(Application.loadedLevel);
    }
    private void Update()
    {
        if (!Advertisement.IsReady()&&!PM.canPlay)
        {
            buf_add_button.gameObject.SetActive(false);
            rew_anim.SetActive(false);
        }
        else if (Advertisement.IsReady() && !PM.canPlay)
        {
            buf_add_button.gameObject.SetActive(true);
            rew_anim.SetActive(true);
        }
        if (CC.start_game && PM.canPlay)
        {
            //points = BR.count;
            points += platform_speed * ScoreMultiplier * Time.deltaTime;
            platform_speed += .1f * Time.deltaTime;
            platform_speed = Mathf.Clamp(platform_speed, 10, maxSpeed);

            result_score = format_for_score + (int)points;
            st_result_score = result_score.ToString();
            score_format = string.Format("{0}{1}:{2}{3}:{4}{5}", st_result_score[1], st_result_score[2], st_result_score[3], st_result_score[4], st_result_score[5], st_result_score[6]);
            scoreTxt.text = score_format;
        }

        if (deth_font_size_anim)
        {

            if (deth_double_up_txt.fontSize >= 100)
                deth_font_size = false;
            if (deth_double_up_txt.fontSize <= 75)
                deth_font_size = true;

            if (deth_font_size)
            {
                deth_double_up_txt.fontSize += 1f;
            }
            if (!deth_font_size)
            {
                deth_double_up_txt.fontSize -= 1f;
            }
        }

        if (PM.speedPU_active)
        {
            StartCoroutine(SpeedUP());
        }
        if (CC.start_game)
        {
            PM.GetComponent<Animator>().enabled = false;
            PM.GetComponent<playerContol>().enabled = true;
        }
    }
    public void buff_name_func()
    {
        if (PM.speedPU_active || PM.drone_buff || PM.multy_buff)
        {
            buff_name_plate.SetActive(true);
            switch (PM.buff_number)
            {
                case 1:
                    buff_name = "speed increased ";
                    StartCoroutine(buff_text());
                    break;
                case 2:
                    buff_name = "score increased ";
                    StartCoroutine(buff_text());
                    break;
                case 3:
                    buff_name = "drone active ";
                    StartCoroutine(buff_text());
                    break;
            }
        }
        else
            buff_name_plate.SetActive(false);
    }
    IEnumerator buff_text()
    {
        yield return new WaitForSeconds(1.2f);
        buff_name_obj.SetActive(true);
        for (int i = 0; i < buff_name.Length; i++)
        {
            current_text = buff_name.Substring(0, i);
            buff_name_txt.text = current_text;
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(0.2f);
        buff_name_obj.SetActive(false);
    }
    IEnumerator SpeedUP()
    {
        IncreaseSpeed();
        yield return new WaitForSeconds(10f);
        DecriseSpeed();
    }
    public void IncreaseSpeed()
    {
        if (Camera.main.fieldOfView < 100)
        {
            Camera.main.fieldOfView += 1f;
            sky_star.gameObject.transform.localScale += new Vector3(0.25f, 0f, 0.25f);
            sky_star.transform.position += new Vector3(0, 1.25f, 0);
            leftDestr.transform.position -= new Vector3(0, 0, 2f);
            rightDestr.transform.position -= new Vector3(0, 0, 2f);
            maxSpeed = 40f;
            if (platform_speed < 40)
            {
                platform_speed += 1f;
            }
        }
    }
    public void DecriseSpeed()
    {
        if (Camera.main.fieldOfView > 65)
        {
            Camera.main.fieldOfView -= 1f;
            sky_star.gameObject.transform.localScale -= new Vector3(0.25f, 0f, 0.25f);
            sky_star.transform.position -= new Vector3(0, 1.25f, 0);
            leftDestr.transform.position += new Vector3(0, 0, 2f);
            rightDestr.transform.position += new Vector3(0, 0, 2f);
            maxSpeed = 20f;
            if (platform_speed > takeTimeSpeed)
            {
                platform_speed -= 1f;
            }
        }
    }
    public void ShowResult()
    {
        AudioManager.Insctance.crush_SE();
        if (!Advertisement.IsReady())
        {
            deth_coins_offer.gameObject.SetActive(false);
            doble_coins_btn.gameObject.SetActive(false);
        }
        else
        {
            deth_coins_offer.gameObject.SetActive(true);
            doble_coins_btn.gameObject.SetActive(true);
        }
        deth_font_size_anim = true;
        Result.SetActive(true);
        play_txt.SetActive(false);
        distance.SetActive(false);
        coins_ui.SetActive(false);
        stop.SetActive(true);
        stop_ic.SetActive(true);
        statistic();
        rew_cost_txt.text = rewind_buff_cost.ToString();
        if (result_bool)
        {
            deth_dis.text = score_format;
            deth_coins.text = coins_format;
            result_bool = false;
            TR.SetTrigger("escape");
        }

        result_rew_buff_txt.text = rewind_buff_count.ToString();
        if (rewind_buff_count - rewind_buff_cost >= 0)
        {
            rewind_btn.text = "rewind";
        }
        else
        {
            rewind_btn.text = "not enought rewind";
        }
        SaveManager.Instance.SaveGame();
    }
    public void final_coins_result()
    {
        coins = coins + run_coins;
        SaveManager.Instance.SaveGame();
    }
    public void AddCoins(int number)
    {
        run_coins += number;
        coinsFormatter(run_coins);
        coinsTxt.text = coins_format;
    }
    public void coinsFormatter(int tmpCoin)
    {
        result_coins = format_for_coins + (int)tmpCoin;
        st_result_coins = result_coins.ToString();
        coins_format = string.Format("{0}{1}:{2}{3}", st_result_coins[1], st_result_coins[2], st_result_coins[3], st_result_coins[4]);
    }
    public void AddRewindBuff(int buf_number)
    {
        rewind_buff_count += buf_number;
        rew_anim.SetActive(true);
        rew_anim.GetComponent<Animator>().SetTrigger("add_count");
    }
    public void statistic()
    {
        int temp1 = (int)points;
        if (temp1 > stats[8])
        {
            stats[8] = temp1;
        }
        Array.Sort(stats);
        Array.Reverse(stats);
    }
    public void ActivateSkins(int skinIndex)
    {
        foreach (var skin in Skins)
        {
            skin.HideSkin();
        }
        Skins[skinIndex].ShowSkin();
        PM.SkinAnimator = Skins[skinIndex].AC;
    }

    public void ActivatePlayerSkins(int PlayerskinIndex)
    {
        foreach (var playerSkin in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (playerSkin.name == "player_skin_1")
            {
                playerSkin.SetActive(false);
            }
            if (playerSkin.name == "player_skin_2")
            {
                playerSkin.SetActive(false);
            }
            if (playerSkin.name == "player_skin_3")
            {
                playerSkin.SetActive(false);
            }
        }
        string[] str_tmp = { "player_skin_2", "player_skin_3", "player_skin_1" };
        foreach (var playerSkin in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (playerSkin.name == str_tmp[PlayerskinIndex])
            {
                playerSkin.SetActive(true);
            }
        }
    }

    public void rewind()
    {
        if (rewind_buff_count - rewind_buff_cost >= 0)
        {
            rewind_buff_count -= rewind_buff_cost;
            rewind_buff_cost *= 2;
            TR.SetTrigger("rewind");
            AudioManager.Insctance.casete_SE();
            foreach (var other in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (other.gameObject.name == "monitor")
                {
                    Item_spawner_parabola.Instance.Monitor.Push(other.gameObject);
                    other.gameObject.SetActive(false);
                }
                else if (other.gameObject.name == "arcanoid")
                {
                    Items_spawner.Instance.Arcanoid.Push(other.gameObject);
                    other.gameObject.SetActive(false);
                }
                else if (other.gameObject.name == "pc_block")
                {
                    Item_spawner_parabola.Instance.PC_block.Push(other.gameObject);
                    other.gameObject.SetActive(false);
                }
                else if (other.gameObject.name == "server_stand")
                {
                    Items_spawner.Instance.Server_stand.Push(other.gameObject);
                    other.gameObject.SetActive(false);
                }
                else if (other.gameObject.name == "vidik")
                {
                    Item_spawner_parabola.Instance.Vidik.Push(other.gameObject);
                    other.gameObject.SetActive(false);
                }
                else if (other.gameObject.tag == "cube")
                {
                    other.gameObject.SetActive(false);
                }
            }
            StartCoroutine(rewind_truck_time());
            PM.deth_fix = true;
            result_bool = true;
            Result.SetActive(false);
            play_txt.SetActive(true);
            distance.SetActive(true);
            coins_ui.SetActive(true);
            stop.SetActive(false);
            stop_ic.SetActive(false);
            PlatfomSpawner.Instance.spawnonnextplatf = false;
        }
        else
        {
            return;
        }

    }
    // ADS
    public void Request_Extra_life()
    {
        if (Advertisement.IsReady())
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = Extra_life;
            Advertisement.Show("rewardedVideo", so);
            music_point = AudioManager.Insctance.BGS.time;
            AudioManager.Insctance.BGS.volume = 0;
            AudioManager.Insctance.SoundEffects.volume = 0;
        }
    }
    public void Extra_life(ShowResult sr1)
    {
        if (sr1 == UnityEngine.Advertisements.ShowResult.Finished)
        {
            rewind_buff_count += 1;
            SaveManager.Instance.SaveGame();
            rew_buff_txt.text = rewind_buff_count.ToString();
            AudioManager.Insctance.BGS.time = music_point;
            AudioManager.Insctance.BGS.volume = 0.3f;
            AudioManager.Insctance.SoundEffects.volume = 0.5f;
        }
        else if (sr1 == UnityEngine.Advertisements.ShowResult.Skipped)
        {
            //"You skipped the add for no reward"  
        }
        else if (sr1 == UnityEngine.Advertisements.ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
    public void Request_Coins()
    {
        if (Advertisement.IsReady())
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = Coins_Ad;
            Advertisement.Show("rewardedVideo", so);
            music_point = AudioManager.Insctance.BGS.time;
            Debug.Log(music_point);
            AudioManager.Insctance.BGS.volume = 0;
            AudioManager.Insctance.SoundEffects.volume = 0;
        }
    }
    public void Coins_Ad(ShowResult sr2)
    {
        if (sr2 == UnityEngine.Advertisements.ShowResult.Finished)
        {
            run_coins *= 2;
            coinsFormatter(run_coins);
            deth_coins.text = coins_format;
            doble_coins_text.text = "Doubled";
            doble_coins_btn.interactable = false;
            rewind_btn.text = "you can't rewind";
            rew_btn.interactable = false;
            AudioManager.Insctance.BGS.time = music_point;
            Debug.Log(AudioManager.Insctance.BGS.time);
            AudioManager.Insctance.BGS.volume = 0.3f;
            AudioManager.Insctance.SoundEffects.volume = 0.5f;
        }
        else if (sr2 == UnityEngine.Advertisements.ShowResult.Skipped)
        {
            //"You skipped the add for no reward"  
        }
        else if (sr2 == UnityEngine.Advertisements.ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
    public void Request_On_Death()
    {
        if (Advertisement.IsReady())
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = Death_ad;
            Advertisement.Show("video", so);
            death_when_add = true;
            music_point = AudioManager.Insctance.BGS.time;
            AudioManager.Insctance.BGS.volume = 0;
            AudioManager.Insctance.SoundEffects.volume = 0;
        }
    }
    public void Death_ad(ShowResult sr3)
    {
        if (sr3 == UnityEngine.Advertisements.ShowResult.Finished)
        {
            AudioManager.Insctance.BGS.time = music_point;
            AudioManager.Insctance.BGS.volume = 0.05f;
            AudioManager.Insctance.SoundEffects.volume = 0.25f;
        }
        else if (sr3 == UnityEngine.Advertisements.ShowResult.Skipped)
        {
            AudioManager.Insctance.BGS.time = music_point;
            AudioManager.Insctance.BGS.volume = 0.05f;
            AudioManager.Insctance.SoundEffects.volume = 0.25f;
        }
        else if (sr3 == UnityEngine.Advertisements.ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
    // ADS
    IEnumerator rewind_truck_time()
    {
        rewind_active = true;
        rew_icon.SetActive(true);
        yield return new WaitForSeconds(2f);
        PM.canPlay = true;
        rewind_active = false;
        rew_icon.SetActive(false);
    }
}
