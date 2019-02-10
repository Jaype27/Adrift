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
	private float _moveInput;
	public Slider oxygenMeter;
	public float m_maxOxygen = 20f;
	public GameObject m_oxygenUI;
	public float m_currentOxygen = 20f;
	public GameObject winSprite;
	public RaftMove theRaft;
	public float pointsToAdd;
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

	void FixedUpdate() {
		_moveInput = Input.GetAxisRaw("Horizontal");
		m_rb.velocity = new Vector2(_moveInput * m_movement, m_rb.velocity.y);
	}
	
	void Update () {

		if(_moveInput > 0) {
			transform.eulerAngles = new Vector3(0, 0, 0);
			anim.SetFloat("Speed", m_movement);
		} else if(_moveInput < 0) {
			transform.eulerAngles = new Vector3(0, 180, 0);
			anim.SetFloat("Speed", m_movement);
		} else {
			anim.SetFloat("Speed", 0);
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
			
			anim.SetBool("IntheWater", true);
		} else {
			anim.SetBool("IntheWater", false);
		}

		if(m_currentOxygen <= 0) {
			gm.PlayerDeath(this);
			gm.m_lives--;
			actionSounds[0].Play();

			woodCollected = 0;
			ropeCollected = 0;
			m_currentOxygen = m_maxOxygen;

			gm.Respawn();
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
		}

		if(other.gameObject.tag == "Rope") {
			other.gameObject.SetActive(false);
			actionSounds[1].Play();
			ropeCollected++;
		}
	}

	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "Win" && ropeCollected == 2 && woodCollected == 3) {
			this.gameObject.SetActive(false);
			GameManager.AddPoints(pointsToAdd);
			winSprite.gameObject.SetActive(true);
			woodCollected = 0;
			ropeCollected = 0;
			theRaft.WinAnim();
		}

		if(other.gameObject.tag == "KillZone") {
			
			gm.PlayerDeath(this);
			gm.m_lives--;
			actionSounds[0].Play();

			woodCollected = 0;
			ropeCollected = 0;
			m_currentOxygen = m_maxOxygen;

			gm.Respawn();
		}
	}
}
