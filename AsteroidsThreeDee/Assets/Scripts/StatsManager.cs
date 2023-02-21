using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//David Richman

public class StatsManager : MonoBehaviour
{

    private const string gameScoreKey = "game_score";
    private const string highScoreKey = "high_score";

    public float health = 200;
    public float lerpHealth = 0.99f;
    public UnityEngine.UI.Image healthBar;
    public TMPro.TMP_Text scoreText;
    private float maximum;
    private float fill;
    int score = 0;
    int pastScore = 0;

    public void IncrementScore(int add)
    {
        score += add;

    }

    // Start is called before the first frame update
    void Start()
    {
        maximum = health;
        if (PlayerPrefs.HasKey(highScoreKey)) //key exists
        {
            pastScore = PlayerPrefs.GetInt(highScoreKey);
            Debug.Log("Found score " + pastScore);
        }
        else
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            Debug.Log("Setting high score");
        }
        if (!PlayerPrefs.HasKey(gameScoreKey))
        {
            PlayerPrefs.SetInt(gameScoreKey, pastScore);
        }
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if (health <= 0)
        {
            fill = 0;
            //gameOver
            PlayerPrefs.SetInt(gameScoreKey, score);
            if (score > pastScore)
            {
                //new highscore
                PlayerPrefs.SetInt(highScoreKey, score);
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");

        }
        fill = Mathf.Lerp(fill, health, lerpHealth);
        healthBar.fillAmount = fill / maximum;
    }
}
