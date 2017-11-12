using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static float m_score;
	public float m_highscore;
	public float m_lives;
	public Text m_scoreUIText;
	public Text m_livesUIText;
	public Text m_highscoreUIText;
	public Text m_gameoverUIText;

	public PlayerControl thePlayer;
	public GameObject spawnPoint;

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
		
		m_score = 0f;
		m_highscore = 0f;
		m_lives = 2f;


// TODO: UNCOMMENT IN THE END
		// m_highscore = PlayerPrefs.GetFloat("Highscore");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		m_scoreUIText.text = "Score: " + m_score;
		m_highscoreUIText.text = "Highscore: " + m_highscore;
		m_livesUIText.text = "Lives: " + m_lives;
	}

	public static void AddPoints (float pointsToAdd) {
		m_score += pointsToAdd;
	}

	public void PlayerDeath (PlayerControl player) {
		thePlayer.gameObject.SetActive(false);
		//Destroy (player.gameObject);
	}
	
	public void Respawn () {
		
		StartCoroutine(Retry());

	}
	public IEnumerator Retry () {
		
		if(m_lives >= 1) {
			yield return new WaitForSeconds(2f);
			thePlayer.transform.position = spawnPoint.transform.position;
			// Instantiate (thePlayer, spawnPoint.position, spawnPoint.rotation);
			thePlayer.gameObject.SetActive(true);
			m_lives--;
		} else if(m_lives <= 1) {
			thePlayer.gameObject.SetActive(false);
			Debug.Log("Game Over");
			
		}
			

	}
}
