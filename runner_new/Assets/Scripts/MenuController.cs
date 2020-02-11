using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuController : MonoBehaviour
{
    public GameObject Music_obj, scanline_obj;
    public gameManager GM;
    public bool Music = true, Sound = true, Scanline=true;
    public Sprite chek, unchek;
    public Image music_btn,sound_effect_btn,scnaline_btn;
    public TextMeshProUGUI stats_txt;
    public Animator menu_buff;
    private void Start()
    {
        if (Scanline)
        {
            scanline_obj.SetActive(true);
        }
        else
            scanline_obj.SetActive(false);
        music_btn.sprite = Music ? chek : unchek;
        scnaline_btn.sprite = Scanline ? chek : unchek;
        sound_effect_btn.sprite = Sound ? chek : unchek;
        StartCoroutine(buff_rotate());
    }
    public void settings_music() {
        Music = !Music;
        music_btn.sprite = Music ? chek : unchek;
        SaveManager.Instance.SaveGame();
    }
    public void settings_scan_line()
    {
        Scanline = !Scanline;
        scnaline_btn.sprite = Scanline ? chek : unchek;
        if (Scanline)
        {
            scanline_obj.SetActive(true);
        }
        else
            scanline_obj.SetActive(false);
        SaveManager.Instance.SaveGame();
    }
    public void settings_sound_effect()
    {
        Sound = !Sound;
        sound_effect_btn.sprite = Sound ? chek : unchek;
        SaveManager.Instance.SaveGame();
    }
    public void stats_formater() {
        int[] tmp = GM.stats;
        int format_for_stats = 1000000;
        string stats_line_form;
        string st_res_stats;
        string[] str_tmp = { "", "", "", "", "", "", "", "", "", ""};
        for (int i = 0; i < tmp.Length; i++) {
            int res_stats = 0;
            res_stats = format_for_stats + tmp[i];
            st_res_stats = res_stats.ToString();
            stats_line_form=string.Format("{0}{1}:{2}{3}:{4}{5}", st_res_stats[1], st_res_stats[2], st_res_stats[3], st_res_stats[4], st_res_stats[5], st_res_stats[6]);
            str_tmp[i] = stats_line_form;
        }
        string stats_format = string.Format("1.    {0}\n2.    {1}\n3.    {2}\n4.    {3}\n5.    {4}\n6.    {5}\n7.    {6}\n8.    {7}\n9.    {8}", str_tmp[0], str_tmp[1], str_tmp[2], str_tmp[3], str_tmp[4], str_tmp[5], str_tmp[6], str_tmp[7], str_tmp[8]);
        stats_txt.text = stats_format;
    }
    IEnumerator buff_rotate() {
        menu_buff.SetTrigger("rotate");
        yield return new WaitForSeconds(5f);
        StartCoroutine(buff_rotate());
    }
    public void hide_menu_buff() {
        menu_buff.SetTrigger("hide");
    }
}
