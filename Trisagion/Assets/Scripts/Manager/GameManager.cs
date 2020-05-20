using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public static int playerHp = 99;
    public static int playerHpMax = 99;
    public static float playerMp = 4;
    public static float playerMpMax = 4;
    public static int level;
    public static int levelNow = 1;
    public static int score = 0;
    public static float time = 0;
    public static int personChoose = 2;


    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }
       
    public static void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerHp = 99;
        playerMp = 4;
        score = 0;
    }

    public static void PlayerWin()
    {
        playerHp = 99;
        playerMp = 4;
        levelNow += 1;
        SceneManager.LoadScene("Account");
    }

    public static void nextScene()
    {
        score = 0;
        time = 0;
        SceneManager.LoadScene("Level"+levelNow);
    }

    public static void returnScene()
    {
        playerHp = 99;
        playerMp = 4;
        score = 0;
        time = 0;
        SceneManager.LoadScene("Menu 3D");
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
