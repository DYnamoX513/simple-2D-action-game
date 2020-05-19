using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptCancel : MonoBehaviour
{
	public void GameCancel () 
	{
		SceneManager.LoadScene("Menu 3D");
	}
}
