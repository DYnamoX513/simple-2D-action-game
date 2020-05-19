using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptB : MonoBehaviour
{
	public void GameB () 
	{
		if (GameManager.levelNow >= 2)
		{
			GameManager.level = 2;
			SceneManager.LoadScene("Mem");
		}
	}
}