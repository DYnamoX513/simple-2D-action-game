using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptA : MonoBehaviour
{
	public void GameA () 
	{
		if(GameManager.levelNow >= 1)
		{
			GameManager.level = 1;
			SceneManager.LoadScene("Mem");
		}
	}
}