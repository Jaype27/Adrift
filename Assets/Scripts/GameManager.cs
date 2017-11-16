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
		
		m_score = 0f;
		m_highscore = 0f;
		m_lives = 2f;
		Respawn();
		StartCoroutine(SpawnWaves());

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
	}
	
	public void Respawn () {
		
		StartCoroutine(Retry());

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
			
			m_lives--;
		} else if(m_lives <= 1) {
			thePlayer.gameObject.SetActive(false);
			Debug.Log("Game Over");
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
