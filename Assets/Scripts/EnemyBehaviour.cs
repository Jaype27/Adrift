using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	public float m_speed;
	private Rigidbody m_rb;
	void Start () {
		m_rb = GetComponent<Rigidbody>();
		m_rb.velocity = transform.right * m_speed;
	}
}
