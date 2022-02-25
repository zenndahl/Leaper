using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI timerUI;

    public GameObject menuUI;
    public PlayerController player;

    public float timeTotal;
    private float timerCountdown;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    void Start()
    {
        EventManager.Instance.onAddTime += TimerUpdate;
        player = FindObjectOfType<PlayerController>();
    }

    private void OnDestroy()
    {
        EventManager.Instance.onAddTime -= TimerUpdate;
    }

    public void Play()
    {
        Time.timeScale = 1;
        timerCountdown = timeTotal;
        //PlayerController.points = 0;
        menuUI.SetActive(false);
        SpawnerController.spawn = true;
    }

    public void Quit()
    {
        Quit();
    }

    void Update()
    {
        pointsUI.text = player.ReturnPoints().ToString();

        timerCountdown = Mathf.Clamp(timerCountdown, 0f, Mathf.Infinity);
        timerUI.text = string.Format("{0:00.00}", timerCountdown);
        

        if(timerCountdown <= 0)
        {
            //game over
        }
        timerCountdown -= Time.deltaTime;

    }

    void TimerUpdate(int timeToAdd)
    {
        timerCountdown += timeToAdd;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        menuUI.SetActive(true);
        SpawnerController.spawn = false;
        
    }

    //public static float CheckTimer()
    //{
    //    return timerCountdown;
    //}
}
