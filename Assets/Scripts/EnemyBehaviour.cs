using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	public float m_speed;
	private Rigidbody m_rb;
	private SpriteRenderer m_spriteRender;
	
	void Start () {
		m_rb = GetComponent<Rigidbody>();
		m_spriteRender = GetComponent<SpriteRenderer>();
		m_rb.velocity = transform.right * m_speed;
		
	}

	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Boundary") {
			m_rb.velocity = transform.right * -m_speed;
			m_spriteRender.flipX = true;
		} else {
			//m_rb.velocity = transform.right * m_speed;
		}
	}
}
