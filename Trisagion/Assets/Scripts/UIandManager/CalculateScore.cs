using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CalculateScore : MonoBehaviour
{
    public Text FinalScore;
    public Text Score;
    public Text Time;

    private void OnEnable()
    {
        FinalScore.text = (GameManager.score + (int)GameManager.time * 1000).ToString();
        Score.text = GameManager.score.ToString();
        Time.text = GameManager.time.ToString() + " s";
    }

    public void returnScene()
    {
        GameManager.returnScene();
    }

    public void nextScene()
    {
        GameManager.nextScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
