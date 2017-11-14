using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] m_hazards;
	public Vector3 m_spawnValues;
	public int m_hazardCount;
	public float m_spawnWait;
	public float m_startWait;
	public float m_waveWait;
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

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (m_startWait);
		while (true) {
			for (int i = 0; i < m_hazardCount; i++) { // Stay in the loop as long as i is less than m_hazardCount; every time the loop cycles we incriment i by 1; executing SpawnWaves() loops this as many times as listed in m_hazardCount
				GameObject m_hazard = m_hazards [Random.Range (0, m_hazards.Length)];
				Vector3 m_spawnPosition = new Vector3 (Random.Range (-m_spawnValues.x, m_spawnValues.x), m_spawnValues.y, m_spawnValues.z);
				Quaternion m_spawnRotation = Quaternion.identity;
				Instantiate (m_hazard, m_spawnPosition, m_spawnRotation);
				yield return new WaitForSeconds (m_spawnWait);
			}
			yield return new WaitForSeconds (m_waveWait);
		}
	}
}
