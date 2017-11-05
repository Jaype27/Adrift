using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float m_movement = 0f;
	public float m_jump = 0f;
	private bool m_isWater = false;
//	private bool m_stoppedJumping = true;	
	private Rigidbody m_rb;
	// private GameManager gm;

	
	void Awake () {
		m_rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Use this for initialization
	void Start () {
		// gm = GetComponent<GameManager>();
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
			// m_stoppedJumping = false;
			m_rb.AddForce(Vector3.up * m_jump, ForceMode.Impulse);
			// GetComponent<Rigidbody>().velocity = new Vector3(0, m_jump);
		}
		
		/*if(Input.GetButtonDown("Jump") && !m_stoppedJumping) {
			if(m_rb.velocity.y > 0) {
				Vector3 velocity = m_rb.velocity;
				velocity.y = 0;
				m_rb.velocity = velocity;
			}
			m_stoppedJumping = true;
		}*/
	}

	void OnTriggerStay (Collider other) {
		if(other.gameObject.tag == "Water") {
			m_isWater = true;
			
		}
	}
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "FloorBed") {
			m_isWater = true;
		}
	}

	/*void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "Fish") {
			gm.m_score += 100;
			Destroy(other.gameObject);
		}
	}*/
}
