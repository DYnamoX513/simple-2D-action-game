using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemB : MonoBehaviour
{
	public void MemberB () 
	{
		GameManager.personChoose = 2;
		SceneManager.LoadScene("Level" + GameManager.level);
	}
}

