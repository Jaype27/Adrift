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

	void OnTriggerEnter (Collider other) {
		//Debug.Log(gameObject.name+" collide with "+other.gameObject.name);
		if(other.gameObject.tag == "Boundary") {
		//	Debug.Log("before vel:"+m_rb.velocity);
			m_rb.velocity = m_rb.velocity * -1;
			m_spriteRender.flipX = !m_spriteRender.flipX;
		//	Debug.Log("after vel:"+m_rb.velocity);
			
		} else {
		//	Debug.Log("else");
			//m_spriteRender.flipX = false;
			//m_rb.velocity = transform.right * m_speed;
		}
		if(other.gameObject.tag == "BoundaryTwo") {
			m_rb.velocity = transform.right * m_speed;
			m_spriteRender.flipX = false;
		} else {
			//m_rb.velocity = transform.right * m_speed;
		}
	}
}
