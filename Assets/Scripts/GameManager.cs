using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text m_scoreUIText;
	public Text m_livesUIText;
	public Text m_highscoreUIText;

	public static GameManager Instance { get { return m_instance; } }
	private static GameManager m_instance = null;

	
	void Awake() {
		if (m_instance != null && m_instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			m_instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	
	
	// Use this for initialization
	void Start () {
		m_scoreUIText.text = "Score: ";
		m_highscoreUIText.text = "Highscore: ";
		m_livesUIText.text = "Lives: ";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
