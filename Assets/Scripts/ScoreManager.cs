using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    #region Instance
    public static ScoreManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion



    public int score = 0;
    public int scoreBonus = 1000;

    public float gameoverDelay = 5;

    float gameoverCountDown = 0;

    public Text scoreText;

    private void Start()
    {
        Time.timeScale = 1.0f;
        gameoverCountDown = 0;

        scoreText.text = $"Score : {score}";
    }


    public void AddScore()
    {
        score += scoreBonus;

        scoreText.text = $"Score : {score}";
    }

    public void TryEnddingGame(bool b)
    {
        if (!b)
        {
            if (gameoverCountDown < gameoverDelay)
            {
                gameoverCountDown += Time.deltaTime;
            }
            else
            {
                Time.timeScale = 0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            gameoverCountDown = 0;
        }
    }
}
