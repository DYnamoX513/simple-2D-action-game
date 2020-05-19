using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuButton: MonoBehaviour
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnExit;


void Start(){
    btnStart.onClick.AddListener(StartGame);
    btnSetting.onClick.AddListener(SettingGame);
    btnExit.onClick.AddListener(ExitGame);
}

public void StartGame(){
    SceneManager.LoadScene("Level1");
}

public void SettingGame(){
    SceneManager.LoadScene("Setting");
}

public void ExitGame(){
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
}

}