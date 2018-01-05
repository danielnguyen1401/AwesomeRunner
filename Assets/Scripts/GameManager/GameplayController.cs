using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [SerializeField] private Text healthText, scoreText, levelText;
    [SerializeField] private GameObject pauseButton;
   [SerializeField] private AudioSource audioSource;
    [HideInInspector] public bool canCountScore;

    private float score, level, health;

    private BGScroller bgScroller;
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject pausePanel;

    void Awake()
    {
        MakeInstance();
        bgScroller = GameObject.Find(Tags.BACKGROUND_TAG).GetComponent<BGScroller>();
        playerMovement = GameObject.Find(Tags.PLAYER_TAG).GetComponent<PlayerMovement>();
        pausePanel.SetActive(false);
    }

    void Start()
    {
        if (GameManager.instance.canPlayMusic)
        {
            audioSource.volume = 0.2f;
            audioSource.Play();
        }
    }

    void Update()
    {
//        IncrementScore(1f);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
        instance = null;
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Tags.GAMEPLAY_SCENE)
        {
            if (GameManager.instance.gameStartedFromMainMenu)
            {
                GameManager.instance.gameStartedFromMainMenu = false;
                score = 0;
                health = 1;
                level = 0;
            }
            else if (GameManager.instance.gameRestartedPlayerDied)
            {
                GameManager.instance.gameRestartedPlayerDied = false;
                score = GameManager.instance.score;
                health = GameManager.instance.health;
            }
            scoreText.text = score.ToString();
            healthText.text = health.ToString();
            levelText.text = level.ToString();
        }
    }

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
        if (health <= 0)
        {
            health = 0;
            StartCoroutine(PlayerDied(Tags.MAIN_MENU_SCENE));
        }
        healthText.text = health.ToString();
    }

    IEnumerator PlayerDied(string sceneName)
    {
        playerMovement.PlayerDied();
        bgScroller.canScroll = false;
        canCountScore = false;
//        playerAlive = false;

        GameManager.instance.score = score;
        GameManager.instance.health = health;
        GameManager.instance.gameRestartedPlayerDied = true;

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void IncrementScore(float scoreValue)
    {
        if (canCountScore)
        {
            score += scoreValue;
            scoreText.text = score.ToString();
        }
    }

    public void IncremenHealth(int healthValue)
    {
        health += healthValue;
        healthText.text = health.ToString();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGameplay()
    {
        canCountScore = false;
        bgScroller.canScroll = false;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        pauseButton.SetActive(false);
    }

    public void ResumeGameplay()
    {
        bgScroller.canScroll = true;
        canCountScore = true;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        pauseButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Tags.MAIN_MENU_SCENE);
    }

    public void ReloadGameplay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Tags.GAMEPLAY_SCENE);
    }
}