using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public GameObject pause, pause_ic, play,resum_txt,countdown_obj;
    public playerContol PC;
    public Text countdown;
    void Start()
    {
        pausePanel.SetActive(false);
    }
    public void PauseGame()
    {
        if (PC.canPlay) {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            pause.SetActive(true);
            pause_ic.SetActive(true);
            play.SetActive(false);
        }
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pause.SetActive(false);
        pause_ic.SetActive(false);
        play.SetActive(true);
    }
}
