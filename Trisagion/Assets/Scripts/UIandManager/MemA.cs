using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemA : MonoBehaviour
{
	public void MemberA () 
	{
		GameManager.personChoose = 1;
		SceneManager.LoadScene("Level"+GameManager.level);
	}
}
