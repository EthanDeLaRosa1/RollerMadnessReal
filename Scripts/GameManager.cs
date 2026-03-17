using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public InputAction PauseAction;
    public static GameManager gm = null;
    private GameObject player;

    public enum gameStates { Playing, GameOver, BeatLevel, Paused };
    public gameStates gameState = gameStates.Playing;
    gameStates old_game_state;

    public int PlayerScore = 0;
    public int playerHealth = 3;

    public bool canBeatLevel = false;
    public int beatLevelScore = 0;

    public GameObject mainCanvas;
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public GameObject PauseText;
    public GameObject WinText;
    public GameObject GameOverText;
    public GameObject PlayAgainButton;
    public GameObject NextLevelButton;
    public GameObject BackToLevel1Button;   // NEW

    Vector3 PlayerSpawnLocation;

    AudioSource audioSource;
    public AudioClip backgroundSFX;
    public AudioClip gameOverSFX;
    public AudioClip beatLevelSFX;
    GameObject cam;

    void Awake()
    {
        if (gm == null) gm = this;
    }

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        audioSource = cam.GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = cam.AddComponent<AudioSource>();
        }

        PlayAudioRepeat(backgroundSFX);

        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) UnityEngine.Debug.LogError("Player not found in Game Manager");

        PlayerSpawnLocation = player.transform.position;
        setupDefaults();
        PauseAction.Enable();
    }

    void setupDefaults()
    {
        GameOverText.SetActive(false);
        WinText.SetActive(false);
        PauseText.SetActive(false);
        PlayAgainButton.SetActive(false);
        NextLevelButton.SetActive(false);

        if (BackToLevel1Button != null)
            BackToLevel1Button.SetActive(false);

        displayPlayerHealth();
    }

    void displayPlayerHealth()
    {
        healthText.text = "Health: " + playerHealth.ToString();
    }

    void displayPlayerScore()
    {
        scoreText.text = "Score = " + PlayerScore.ToString();
    }

    public void add_score(int amount)
    {
        PlayerScore += amount;
        displayPlayerScore();

        if (canBeatLevel)
        {
            if (PlayerScore >= beatLevelScore)
            {
                WinText.SetActive(true);
                NextLevelButton.SetActive(true);
                PlayAgainButton.SetActive(true);

                if (BackToLevel1Button != null)
                    BackToLevel1Button.SetActive(true);

                audioSource.Stop();
                PlayAudioOnce(beatLevelSFX);
                gameState = gameStates.BeatLevel;
            }
        }
    }

    void Update()
    {
        if (PauseAction.WasPressedThisFrame()) {
            if (gameState == gameStates.Playing) pause();
            else if (gameState == gameStates.Paused) resume();
        }
    }

    void pause()
    {
        PauseText.SetActive(true);
        old_game_state = gameState;
        gameState = gameStates.Paused;
    }

    void resume()
    {
        PauseText.SetActive(false);
        gameState = old_game_state;
    }

    void destroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }

    void destoyAllCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins) {
            Destroy(coin);
        }
    }

    public void dec_health()
    {
        playerHealth -= 1;
        displayPlayerHealth();

        if (playerHealth <= 0) {
            GameOverText.SetActive(true);
            PlayAgainButton.SetActive(true);

            if (BackToLevel1Button != null)
                BackToLevel1Button.SetActive(true);

            gameState = gameStates.GameOver;
            audioSource.Stop();
            PlayAudioOnce(gameOverSFX);
        }
        else
        {
            player.transform.position = PlayerSpawnLocation;
            destroyAllEnemies();
        }
    }

    public void PlayAgain()
    {
        PlayerScore = 0;
        playerHealth = 3;
        displayPlayerHealth();
        displayPlayerScore();
        setupDefaults();
        player.transform.position = PlayerSpawnLocation;
        destroyAllEnemies();
        destoyAllCoins();
        gameState = gameStates.Playing;
        PlayAudioRepeat(backgroundSFX);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void BackToLevel1()   // NEW
    {
        SceneManager.LoadScene("Level1");
    }

    void PlayAudioOnce(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void PlayAudioRepeat(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}