using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Insctance;
    public gameManager GM;
    public MenuController MC;
    public AudioSource BGS, SoundEffects,credits_music;
    public AudioClip coins_se, truck_se, door_se,door_close_se, delor_se,casete_se,crush_se,buff_pickup_se,rewind_se;
    public List<AudioClip> BG_tracklist;
    public List<int> BGTS_number, BGTS_number_sample;
    public GameObject credits;
    int rand_music = 0, last_rand=0;
    float BGS_timme;
    private void Awake()
    {
        if (Insctance == null)
        {
            Insctance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Update()
    {

        if (!BGS.isPlaying&&MC.Music&&!credits.activeSelf)
        {
            Track_changer();
        }
    }
    public void Track_changer() {
        if (BGTS_number.Count < 1) {
            refresh_sound_list();
        }
        rand_music = Random.Range(0, BGTS_number.Count - 1);
        BGS.Stop();
        BGS.clip = BG_tracklist[BGTS_number[rand_music]];
        BGS.Play();
        BGTS_number.RemoveAt(rand_music);
    }
    public void refresh_sound_list() {
        for (int i = 0; i < BGTS_number_sample.Count; i++)
            BGTS_number.Add(BGTS_number_sample[i]);
    }
    public void stop_music() {
        BGS.Stop();
    }
    public void coins_SE()
    {
        if (MC.Sound)
        {
            SoundEffects.PlayOneShot(coins_se);
        }
    }
    public void truck_SE() {
        if (MC.Sound)
        {
            SoundEffects.PlayOneShot(truck_se);
        }
    }
    public void door_SE() {
        if (MC.Sound)
        {
            SoundEffects.PlayOneShot(door_se);
        }
    }
    public void delor_SE() {
        if (MC.Sound)
        {
            SoundEffects.PlayOneShot(delor_se);
        }
    }
    public void buff_pickup_SE()
    {
        if (MC.Sound)
        {
            SoundEffects.PlayOneShot(buff_pickup_se);
        }
    }
    public void door_close_SE()
    {
        if (MC.Sound)
        {
            SoundEffects.PlayOneShot(door_close_se);
        }
    }
    public void casete_SE()
    {
        if (MC.Sound)
        {
            BGS.volume = 0.3f;
            SoundEffects.PlayOneShot(casete_se);
            StartCoroutine(rewind_delay());
        }
    }
    public void crush_SE() {
        
        if (MC.Sound)
        {
            SoundEffects.volume = 0.25f;
            SoundEffects.PlayOneShot(crush_se);
            BGS.volume = 0;
            StartCoroutine(crush_delay());
        }
    }
    public void Credits_music() {
        BGS_timme = BGS.time;
        BGS.Pause();
        credits_music.Play();
    }
    public void back_from_Credits_music()
    {
        credits_music.Stop();
        BGS.UnPause();
        BGS.time = BGS_timme;
    }
    IEnumerator crush_delay()
    {
        yield return new WaitForSeconds(2.6f);
        if (!GM.death_when_add)
        {
            SoundEffects.volume = 0.5f;
            BGS.volume = 0.05f;
        }
        else
        {
            GM.death_when_add = false;
        }
    }
        IEnumerator rewind_delay()
    {
        BGS.volume = 0f;
        SoundEffects.PlayOneShot(rewind_se);
        yield return new WaitForSeconds(2.2f);
        BGS.volume = 0.3f;
    }
}
