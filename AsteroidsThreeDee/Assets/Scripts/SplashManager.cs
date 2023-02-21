using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//David Richman
public class SplashManager : MonoBehaviour
{
    public UnityEngine.UI.Button button;
    public AudioSource buttonSound;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(StartGame);    
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
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
    
}
