using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public MouseMover[] mice;

    public float timer = 0f;
    public float scoreInterval = 0.5f; 

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= scoreInterval)
        {
            foreach (MouseMover mouse in mice)
            {
                mouse.IncreaseScore();
            }

            scoreText.text = "Score: " + mice[0].score;
            timer = 0f;
        }
    }
}
