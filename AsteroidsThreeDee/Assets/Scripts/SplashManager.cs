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
        yield return new WaitForSeconds(1.4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Debug.Log(Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
