using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSwim : MonoBehaviour {

	public float m_dodge;
	public float m_smoothing;
	public Vector2 m_startWait;
	public Vector2 m_maneuverTime;
	public Vector2 m_maneuverWait;
	public Boundary m_boundary;

	private float m_currentSpeed;
	private float m_targetManeuver;
	private Rigidbody m_rb;

	void Start () {
		m_rb = GetComponent<Rigidbody> ();
		m_currentSpeed = m_rb.velocity.x;
		StartCoroutine (Evade ());
	}

	IEnumerator Evade () {
		yield return new WaitForSeconds (Random.Range (m_startWait.x, m_startWait.y));

		while (true) {
			m_targetManeuver = Random.Range (1, m_dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds (Random.Range (m_maneuverTime.x, m_maneuverTime.y));
			m_targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (m_maneuverWait.x, m_maneuverWait.y));
		}
	}

	void FixedUpdate () {
		float m_newManeuver = Mathf.MoveTowards (m_rb.velocity.x, m_targetManeuver, Time.deltaTime * m_smoothing);
		m_rb.velocity = new Vector3 (m_newManeuver, 0.0f, m_currentSpeed);
		m_rb.position = new Vector3 (
			Mathf.Clamp (m_rb.position.x, m_boundary.xMin, m_boundary.xMax),
			Mathf.Clamp (m_rb.position.z, m_boundary.yMin, m_boundary.yMax),
			0.0f
		);
	}
}
