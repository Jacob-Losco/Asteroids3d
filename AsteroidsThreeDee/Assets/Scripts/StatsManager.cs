using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//David Richman

public class StatsManager : MonoBehaviour
{
    public float health = 200;
    public float lerpHealth = 0.99f;
    public UnityEngine.UI.Image healthBar;
    private float maximum;
    private float fill;
    int score = 0;

    public void IncrementScore(int add)
    {
        score += add;

    }

    // Start is called before the first frame update
    void Start()
    {
        maximum = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            fill = 0;
            //gameOver
            return;

        }
        fill = Mathf.Lerp(fill, health, lerpHealth);
        try
        {
            healthBar.fillAmount = fill / maximum;
        }
        catch
        {

        }
    }
}
