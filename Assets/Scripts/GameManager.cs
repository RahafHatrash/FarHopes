using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pause Settings")]
    public GameObject pauseMenu;
    public static bool isPaused = false;

    [Header("Score Settings")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;
    public float scoreSpeed = 1f;

    private float currentScore = 0f;
    private static int sessionHighScore = 0;
    private bool isGameOver = false;

    [Header("Size Swapper Settings")]
    public Transform mouseA;
    public Transform mouseB;
    public Vector3 defaultSize = new Vector3(1f, 1f, 1f);
    public Vector3 bigSize = new Vector3(1.5f, 1.5f, 1f);
    public Vector3 smallSize = new Vector3(0.8f, 0.8f, 1f);
    public float smoothSpeed = 5f;

    private bool hasSwapped = false;
    private bool isASmall = true;
    private Vector3 targetScaleA;
    private Vector3 targetScaleB;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize mice sizes
        if (mouseA != null && mouseB != null)
        {
            mouseA.localScale = defaultSize;
            mouseB.localScale = defaultSize;
            targetScaleA = defaultSize;
            targetScaleB = defaultSize;
        }
    }

    void Update()
    {
        HandlePauseInput();
        HandleRestartShortcut();
        HandleScoreUpdate();
        AnimateSizeTransition();
        HandleSizeSwapping();
    }

    void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void HandleRestartShortcut()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
    }

    void HandleScoreUpdate()
    {
        // if (isGameOver || isPaused) return;
        if (isGameOver) return;
        currentScore += scoreSpeed * Time.deltaTime;

        if (scoreText != null)
            scoreText.text = Mathf.FloorToInt(currentScore).ToString();
    }

    void HandleSizeSwapping()
    {
        if (isPaused || isGameOver || mouseA == null || mouseB == null) return;

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !hasSwapped)
        {
            isASmall = true;
            SetTargets();
            hasSwapped = true;
        }
        else if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && hasSwapped)
        {
            isASmall = !isASmall;
            SetTargets();
        }
    }

    void AnimateSizeTransition()
    {
        if (mouseA != null && mouseB != null)
        {
            mouseA.localScale = Vector3.Lerp(mouseA.localScale, targetScaleA, Time.deltaTime * smoothSpeed);
            mouseB.localScale = Vector3.Lerp(mouseB.localScale, targetScaleB, Time.deltaTime * smoothSpeed);
        }
    }

    void SetTargets()
    {
        if (isASmall)
        {
            targetScaleA = smallSize;
            targetScaleB = bigSize;
        }
        else
        {
            targetScaleA = bigSize;
            targetScaleB = smallSize;
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        int intScore = Mathf.FloorToInt(currentScore);

        if (intScore > sessionHighScore)
        {
            sessionHighScore = intScore;
        }

        finalScoreText.text = intScore.ToString();
        highScoreText.text = sessionHighScore.ToString();

        gameOverPanel.SetActive(true);
        if (scoreText != null)
            scoreText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        currentScore = 0f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("game");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        sessionHighScore = 0;
        SceneManager.LoadScene("MainMenu");
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
