using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameManager m_gm;
	public GameObject gameOver;

	public void RetryGame () {
		m_gm.FirstSpawn();
		gameOver.gameObject.SetActive(false);

		
	}
	
	public void Quit () {
		Application.Quit();
		// SceneManager.LoadScene("Menu");
	}
}
