using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private PlayerControls inputActions;
    private PlayerController player;

    private int points = 0;

    //[Header("Sounds")]
    //public SoundAudioClip[] soundAudioClipsArray;
    //public AudioClip bgMusic;

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
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Time.timeScale = 0;
        timerCountdown = timeTotal;
    }

    void Start()
    {
        EventManager.Instance.onAddTime += TimerUpdate;
        EventManager.Instance.onGameOver += GameOver;
        player = FindObjectOfType<PlayerController>();
        inputActions = new PlayerControls();
    }

    private void OnDestroy()
    {
        EventManager.Instance.onAddTime -= TimerUpdate;
    }

    public void Play()
    {
        timerCountdown = timeTotal;
        points = 0;
        menuUI.SetActive(false);
        gameOverUI.SetActive(false);
        SpawnerController.spawn = true;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Quit();
    }

    void Update()
    {
        pointsUI.text = points.ToString();

        timerCountdown = Mathf.Clamp(timerCountdown, 0f, Mathf.Infinity);
        timerUI.text = string.Format("{0:00.00}", timerCountdown);
        

        if(timerCountdown <= 0)
        {
            EventManager.Instance.GameOver();
        }
        timerCountdown -= Time.deltaTime;

    }

    public void AddPoints(int pts)
    {
        points += pts;
    }

    public int GetPoints()
    {
        return points;
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
