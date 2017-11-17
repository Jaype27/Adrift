using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] m_fishes;
	public Vector3 m_spawnValues;
	public int m_fishCount;
	public float m_spawnWait;
	public float m_startWait;
	public float m_waveWait;
	
	public static float m_score;
	public float m_highscore;
	public float m_lives;
	public Text m_scoreUIText;
	public Text m_livesUIText;
	public Text m_highscoreUIText;
	public GameObject gameOver;

	public PlayerControl thePlayer;
	public GameObject spawnPoint;

	public GameObject[] itemList;
	public GameObject[] itemSpawn;

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
		if(PlayerPrefs.HasKey("Highscore")){

		m_highscore = PlayerPrefs.GetFloat("Highscore");

		}
		m_score = 0f;
		m_highscore = 0f;
		m_lives = 3f;

		StartCoroutine(FirstSpawn());
		StartCoroutine(SpawnWaves());
	}
	
	void Update () {
		
		if (m_score > m_highscore) {
			m_highscore = m_score;
			PlayerPrefs.SetFloat("HighScore", m_highscore);
		}
		m_scoreUIText.text = "Score: " + (int)Mathf.Floor(m_score);
		m_highscoreUIText.text = "Highscore: " + Mathf.Floor(m_highscore);


	//	m_scoreUIText.text = "Score: " + m_score;
	//	m_highscoreUIText.text = "Highscore: " + m_highscore;
		m_livesUIText.text = "Lives: " + m_lives;
		
	}

	public static void AddPoints (float pointsToAdd) {
		m_score += pointsToAdd;
	}

	public void PlayerDeath (PlayerControl player) {
		thePlayer.gameObject.SetActive(false);
	}
	
	public void Respawn () {
		
		StartCoroutine(Retry());
	}

	public void StartGame () {

	}
	
	public IEnumerator FirstSpawn () {
		yield return new WaitForSeconds(1f);

			m_score = 0;
			m_lives = 3;

			thePlayer.transform.position = spawnPoint.transform.position;
			thePlayer.gameObject.SetActive(true);
			
			itemList[0].transform.position = itemSpawn[0].transform.position;
			itemList[0].gameObject.SetActive(true);
			
			itemList[1].transform.position = itemSpawn[1].transform.position;
			itemList[1].gameObject.SetActive(true);
			
			itemList[2].transform.position = itemSpawn[2].transform.position;
			itemList[2].gameObject.SetActive(true);
			
			itemList[3].transform.position = itemSpawn[3].transform.position;
			itemList[3].gameObject.SetActive(true);
			
			itemList[4].transform.position = itemSpawn[4].transform.position;
			itemList[4].gameObject.SetActive(true);	
	}

	public IEnumerator Retry () {
		
		if(m_lives >= 1) {
			yield return new WaitForSeconds(2f);
			thePlayer.transform.position = spawnPoint.transform.position;
			thePlayer.gameObject.SetActive(true);
			
			itemList[0].transform.position = itemSpawn[0].transform.position;
			itemList[0].gameObject.SetActive(true);
			
			itemList[1].transform.position = itemSpawn[1].transform.position;
			itemList[1].gameObject.SetActive(true);
			
			itemList[2].transform.position = itemSpawn[2].transform.position;
			itemList[2].gameObject.SetActive(true);
			
			itemList[3].transform.position = itemSpawn[3].transform.position;
			itemList[3].gameObject.SetActive(true);
			
			itemList[4].transform.position = itemSpawn[4].transform.position;
			itemList[4].gameObject.SetActive(true);

			thePlayer.m_currentOxygen = thePlayer.m_maxOxygen;
			
			// m_lives--;
		} else if(m_lives <= 0) {
			
			StartCoroutine(FirstSpawn());
			// m_lives = 3;

			thePlayer.transform.position = spawnPoint.transform.position;
			thePlayer.gameObject.SetActive(true);

			//yield return new WaitForSeconds(1f);
			//thePlayer.gameObject.SetActive(false);

			gameOver.gameObject.SetActive(true);
		}		
	}
	
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (m_startWait);
		while (true) {
			for (int i = 0; i < m_fishCount; i++) { // Stay in the loop as long as i is less than m_hazardCount; every time the loop cycles we incriment i by 1; executing SpawnWaves() loops this as many times as listed in m_hazardCount
				GameObject m_fish = m_fishes [Random.Range (0,m_fishes.Length)];
				Vector3 m_spawnPosition = new Vector3 (Random.Range (-m_spawnValues.x, m_spawnValues.x), m_spawnValues.y, m_spawnValues.z);
				Quaternion m_spawnRotation = Quaternion.identity;
				Instantiate (m_fish, m_spawnPosition, m_spawnRotation);
				yield return new WaitForSeconds (m_spawnWait);
			}
			yield return new WaitForSeconds (m_waveWait);
		}
	}
}
