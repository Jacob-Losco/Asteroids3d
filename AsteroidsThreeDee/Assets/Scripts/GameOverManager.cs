using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private const string gameScoreKey = "game_score";
    private const string highScoreKey = "high_score";

    public TMPro.TMP_Text highScore;
    public TMPro.TMP_Text gameScore;

    private int gameScoreValue;
    private int highScoreValue;

    public UnityEngine.UI.Button button;
    private AudioSource buttonSound;


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(StartGame);
        buttonSound = GetComponent<AudioSource>();
        gameScoreValue = PlayerPrefs.GetInt(gameScoreKey);
        highScoreValue = PlayerPrefs.GetInt(highScoreKey);
        if (gameScoreValue == highScoreValue)
        {
            gameScore.text = "New High Score!\n" + gameScoreValue.ToString();
            highScore.text = "";
        }
        else
        {
            gameScore.text = gameScoreValue.ToString();
            highScore.text = "High Score: " + highScoreValue.ToString();
        }
    }
    void StartGame()
    {
        StartCoroutine(DelayedStart());
    }
    IEnumerator DelayedStart()
    {
        if (!buttonSound.isPlaying)
            buttonSound.Play();
        yield return new WaitUntil(isNotPlaying);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Debug.Log(Time.time);
    }
    bool isNotPlaying()
    {
        return !buttonSound.isPlaying;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
}
