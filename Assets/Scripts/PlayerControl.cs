using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Boundary {
	public float xMin, xMax, yMin, yMax;
}

public class PlayerControl : MonoBehaviour {

	public float m_movement = 0f;
	public float m_jump = 0f;
	public Slider oxygenMeter;
	public float m_maxOxygen = 20f;
	public GameObject m_oxygenUI;
	public float m_currentOxygen = 20f;
	private bool m_isWater = false;	
	private Rigidbody m_rb;
	private GameManager gm;
	private float ropeCollected;
	private float woodCollected;
	
	void Awake () {
		m_rb = gameObject.GetComponent<Rigidbody>();
		gm = FindObjectOfType<GameManager>();
	}
	
	
	void Start () {
		m_currentOxygen = m_maxOxygen;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetAxis("Horizontal") != 0) {
			Vector3 pos = gameObject.transform.position;
			pos.x += Input.GetAxis("Horizontal") * m_movement * Time.deltaTime;
			gameObject.transform.position = pos;
		}

		if(Input.GetButtonDown("Jump") && m_isWater) {
			m_isWater = false;
			m_rb.AddForce(Vector3.up * m_jump, ForceMode.Impulse);
			// GetComponent<Rigidbody>().velocity = new Vector3(0, m_jump);
		}

		if(!m_isWater) {
			m_currentOxygen += Time.deltaTime;
			m_oxygenUI.SetActive(false);
			if(m_currentOxygen >= m_maxOxygen) {
				m_currentOxygen = m_maxOxygen;
			}
			oxygenMeter.value = m_currentOxygen/m_maxOxygen;
		}
		m_isWater = false;
	}

	void OnTriggerStay (Collider other) {
		if(other.gameObject.tag == "Water") {
			m_isWater = true;
			m_oxygenUI.SetActive(true);
			m_currentOxygen -= Time.deltaTime;
			oxygenMeter.value = m_currentOxygen/m_maxOxygen;
		}

		if(m_currentOxygen <= 0) {
			gm.PlayerDeath(this);
			Debug.Log("Player Killed");

			gm.Respawn();
			Debug.Log("Player Spawn");
		}
	}
	
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "FloorBed") {
			m_isWater = true;
		}

		if(other.gameObject.tag == "Wood") {
			Destroy (other.gameObject);
			woodCollected++;
			Debug.Log(woodCollected);
		}

		if(other.gameObject.tag == "Rope") {
			Destroy (other.gameObject);
			ropeCollected++;
			Debug.Log(ropeCollected);
		}
		
		if(other.gameObject.tag == "KillZone") {
			gm.PlayerDeath(this);
			Debug.Log("Player Killed");

	//TODO: RESTART ITEMS TO THEIR SPAWN POINTS

			woodCollected = 0;
			ropeCollected = 0;
			Debug.Log("Lost Items");

			m_currentOxygen = m_maxOxygen;
			Debug.Log("Oxygen Refilled");
			
			gm.Respawn();
			Debug.Log("Player Spawn");
		}
	}

	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "Win" && ropeCollected == 2 && woodCollected == 3) {
			Debug.Log("You Win");
			Time.timeScale = 0.5f;
		}
	}

	//TODO: REMOVEDEBUG.LOGS IN THE END
}
