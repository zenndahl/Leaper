using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerControls inputActions;
    private PlayerController player;

    [Header("Sounds")]
    public SoundAudioClip[] soundAudioClipsArray;
    public AudioClip bgMusic;

    [Header("UI")]
    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI timerUI;
    public GameObject menuUI;
    public GameObject pauseUI;
    public GameObject gameOverUI;


    public float timeTotal;
    private float timerCountdown;
    private bool paused = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 0;
        timerCountdown = timeTotal;

        SoundManager.Initialize();
    }

    void Start()
    {
        EventManager.Instance.onAddTime += TimerUpdate;
        player = FindObjectOfType<PlayerController>();
        inputActions = new PlayerControls();
    }

    private void OnDestroy()
    {
        EventManager.Instance.onAddTime -= TimerUpdate;
    }

    public void Play()
    {
        Time.timeScale = 1;
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
            //GameOver();
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
        gameOverUI.SetActive(true);
        SpawnerController.spawn = false;
        
    }

    public void Pause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            SpawnerController.spawn = false;
            pauseUI.SetActive(true);
            inputActions.Land.Disable();
            paused = !paused;
        }
        else
        {
            Time.timeScale = 1;
            SpawnerController.spawn = true;
            pauseUI.SetActive(false);
            inputActions.Land.Enable();
            paused = !paused;
        }
    }

    //public static float CheckTimer()
    //{
    //    return timerCountdown;
    //}
}
