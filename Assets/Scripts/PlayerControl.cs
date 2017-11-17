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
	public GameObject winSprite;
	public RaftMove theRaft;
	private bool m_isWater = false;	
	Animator anim;
	private Rigidbody m_rb;
	private GameManager gm;
	private float ropeCollected;
	private float woodCollected;
	public AudioSource[] actionSounds;

	
	void Awake () {
		m_rb = gameObject.GetComponent<Rigidbody>();
		gm = FindObjectOfType<GameManager>();
	}
	
	
	void Start () {
		m_currentOxygen = m_maxOxygen;

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetAxis("Horizontal") != 0) {
			Vector3 pos = gameObject.transform.position;
			pos.x += Input.GetAxis("Horizontal") * m_movement * Time.deltaTime;
			gameObject.transform.position = pos;

			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat("Speed", move);
		}

		if(Input.GetButtonDown("Jump") && m_isWater) {
			m_isWater = false;
			m_rb.AddForce(Vector3.up * m_jump, ForceMode.Impulse);
			actionSounds[2].Play();
		}

		if(!m_isWater) {
			m_currentOxygen += Time.deltaTime;
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
			m_currentOxygen -= Time.deltaTime;
			oxygenMeter.value = m_currentOxygen/m_maxOxygen;
			
			bool inWater = m_isWater;
			anim.SetBool("IntheWater", inWater);
		}

		if(m_currentOxygen <= 0) {
			gm.PlayerDeath(this);
			gm.m_lives--;
			actionSounds[0].Play();
			Debug.Log("Player Killed");

			woodCollected = 0;
			ropeCollected = 0;
			Debug.Log("Lost Items");

			m_currentOxygen = m_maxOxygen;
			Debug.Log("Oxygen Refilled");

			gm.Respawn();
			Debug.Log("Player Spawn");
		}
	}
	
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "FloorBed") {
			m_isWater = true;
		}

		if(other.gameObject.tag == "Wood") {
			other.gameObject.SetActive(false);
			actionSounds[1].Play();
			woodCollected++;
			Debug.Log(woodCollected);
		}

		if(other.gameObject.tag == "Rope") {
			other.gameObject.SetActive(false);
			actionSounds[1].Play();
			ropeCollected++;
			Debug.Log(ropeCollected);
		}
	}

	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "Win" && ropeCollected == 2 && woodCollected == 3) {
			this.gameObject.SetActive(false);
			winSprite.gameObject.SetActive(true);
			woodCollected = 0;
			ropeCollected = 0;
			theRaft.WinAnim();
		}

		if(other.gameObject.tag == "KillZone") {
			gm.PlayerDeath(this);
			gm.m_lives--;
			actionSounds[0].Play();
			Debug.Log("Player Killed");

			woodCollected = 0;
			ropeCollected = 0;
			Debug.Log("Lost Items");

			m_currentOxygen = m_maxOxygen;
			Debug.Log("Oxygen Refilled");
			
			gm.Respawn();
			Debug.Log("Player Spawn");
		}
	}

	//TODO: REMOVE DEBUG.LOGS IN THE END
}
