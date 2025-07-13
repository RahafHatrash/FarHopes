using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;

    public float scoreSpeed = 1f;

    private float currentScore = 0f;
private static int sessionHighScore = 0;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (isGameOver) return;

        currentScore += scoreSpeed * Time.deltaTime;

        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(currentScore).ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        int intScore = Mathf.FloorToInt(currentScore);

        // مقارنة وتحديث الهاي سكور داخل نفس الجلسة
        if (intScore > sessionHighScore)
        {
            sessionHighScore = intScore;
        }

        // عرض النتائج
        finalScoreText.text = intScore.ToString();
        highScoreText.text = sessionHighScore.ToString();

        gameOverPanel.SetActive(true);
        if (scoreText != null)
            scoreText.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        currentScore = 0f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
