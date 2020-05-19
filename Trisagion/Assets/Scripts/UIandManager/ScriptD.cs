using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptD : MonoBehaviour
{
	public void GameD () 
	{
		if (GameManager.levelNow >= 4)
		{
			GameManager.level = 4;
			SceneManager.LoadScene("Mem");
		}
	}
}