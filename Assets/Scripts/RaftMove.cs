using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftMove : MonoBehaviour {

	public float m_speed;
	public GameObject raftSprite;
	private Rigidbody m_rb;
	
	void Start () {
		m_rb = GetComponent<Rigidbody>();
		m_rb.velocity = transform.right * m_speed;

		StartCoroutine(Disappear());
	}

	public IEnumerator Disappear () {
		yield return new WaitForSeconds(5f);
		raftSprite.gameObject.SetActive(false);

	}
}
