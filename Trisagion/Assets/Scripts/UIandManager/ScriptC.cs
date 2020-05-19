using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptC : MonoBehaviour
{
	public void GameC () 
	{
		if (GameManager.levelNow >= 3)
		{
			GameManager.level = 3;
			SceneManager.LoadScene("Mem");
		}
	}
}
