using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    private TextMeshProUGUI pointsUI;
    [SerializeField]
    private TextMeshProUGUI timerUI;
    private GameObject menuUI;
    private GameObject pauseUI;
    private GameObject gameOverUI;
    private GameObject pauseButton;

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
    }

    void Start()
    {
        Time.timeScale = 0;

        EventManager.Instance.onAddTime += TimerUpdate;
        EventManager.Instance.onGameOver += GameOver;
        player = FindObjectOfType<PlayerController>();
        inputActions = new PlayerControls();
        SetUIs();
        timerCountdown = timeTotal;
    }

    private void SetUIs()
    {
        pointsUI = GameObject.Find("Pontuação").GetComponent<TextMeshProUGUI>();
        timerUI = GameObject.Find("Tempo").GetComponent<TextMeshProUGUI>();
        menuUI = GameObject.Find("Menu");
        pauseUI = GameObject.Find("PauseUI");
        gameOverUI = GameObject.Find("GameOverMenu");
        pauseButton = GameObject.Find("PauseButton");

        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseButton.SetActive(false);
    }

    public void Play()
    {
        timerCountdown = timeTotal;
        points = 0;
        menuUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseButton.SetActive(true);
        SpawnerController.spawn = true;
        Time.timeScale = 1;
    }

    public void Replay()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        SetUIs();
    }

    public void Quit()
    {
        EventManager.Instance.onAddTime -= TimerUpdate;
        Quit();
    }

    void Update()
    {
        pointsUI.text = points.ToString();

        if(timerUI != null)
        {
            timerCountdown = Mathf.Clamp(timerCountdown, 0f, Mathf.Infinity);
            timerUI.text = string.Format("{0:00.00}", timerCountdown);
        }
        

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
        pauseButton.SetActive(false);
        SpawnerController.spawn = false;
        EventManager.Instance.onAddTime -= TimerUpdate;
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
