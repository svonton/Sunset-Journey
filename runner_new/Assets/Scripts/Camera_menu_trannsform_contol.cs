using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_menu_trannsform_contol : MonoBehaviour
{
    public GameObject camera_obj;
    public GameObject pl_pos;
    public GameObject truc_anim;
    public Animator camera_anim;
    public GameObject right_destr;
    public GameObject left_destr;
    public GameObject menu_obj_group,logo_holder;
    public bool start_game = false;
    public bool start_truck = false;
    public bool spwn_item = true;
    public Animator Door;
    public Animator Star,Star_sec_part;
    public Animator logo_holder_animator;

    public void ftom_menu_to_settings() {
        camera_anim.SetTrigger("menu_to_settings");
        Star.SetTrigger("menu_to_settings");
    }
    public void from_settings_to_menu()
    {
        camera_anim.SetTrigger("settings_to_menu");
        Star.SetTrigger("settings_to_menu");
    }
    public void ftom_menu_to_score()
    {
        camera_anim.SetTrigger("menu_to_stats");
        Star.SetTrigger("menu_to_stats");
    }
    public void from_score_to_menu()
    {
        camera_anim.SetTrigger("stats_to_menu");
        Star.SetTrigger("stats_to_menu");
    }
    public void from_menu_to_shop() {
        camera_anim.SetTrigger("menu_to_shop");
        Star.SetTrigger("menu_to_shop");
    }
    public void from_shop_to_menu()
    {
        camera_anim.SetTrigger("shop_to_menu");
        Star.SetTrigger("shop_to_menu");
    }
    public void from_menu_to_credits() {
        camera_anim.SetTrigger("menu_to_credits");
        Star.SetTrigger("menu_to_credits");
        Star_sec_part.SetTrigger("menu_to_credits");
        logo_holder_animator.SetTrigger("menu_to_credits");
    }
    public void from_credits_to_menu()
    {
        camera_anim.SetTrigger("credits_to_menu");
        Star.SetTrigger("credits_to_menu");
        Star_sec_part.SetTrigger("credits_to_menu");
        logo_holder_animator.SetTrigger("credits_to_menu");
    }
    public void start_condition() {
        camera_anim.SetTrigger("start_game");
        Star.SetTrigger("start_game");
        logo_holder_animator.SetTrigger("start_game");
        StartCoroutine(start_anim());
        StartCoroutine(pl_anim_delay());
        StartCoroutine(start_truck_anim());
        AudioManager.Insctance.truck_SE();
        truc_anim.GetComponent<Animator>().enabled = true;
    }
    IEnumerator start_truck_anim()
    {
        yield return new WaitForSeconds(10);
        spwn_item = false;
        start_truck = true;
    }
    IEnumerator start_anim()
    {
        yield return new WaitForSeconds(5.7f);
        start_game = true;
        Destroy(menu_obj_group);
        Destroy(logo_holder);
        //menu_obj_group.SetActive(false);
        right_destr.SetActive(true);
        left_destr.SetActive(true);

        foreach (var mRoad in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (mRoad.name == "main_road")
            {
                mRoad.GetComponent<Platform_movement>().enabled = true;
            }
        }
    }
    IEnumerator pl_anim_delay() {
        yield return new WaitForSeconds(2f);
        pl_pos.GetComponent<Animator>().enabled = true;
    }

    public void shop_piker() {
        Door.SetTrigger("opening");
        camera_anim.SetTrigger("into_the_shop");
    }
    public void shop_piker_back() {
        Door.SetTrigger("closing");
        camera_anim.SetTrigger("from_shop");
    }
}
