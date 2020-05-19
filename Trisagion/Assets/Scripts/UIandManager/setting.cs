using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class setting: MonoBehaviour
{
    public Button btnBack;

void Start(){
    btnBack.onClick.AddListener(Back);

}

public void Back(){
    SceneManager.LoadScene("Menu");
}

}