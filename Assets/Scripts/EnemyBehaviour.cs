using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public Transform target;
	public Transform m_Transform;
	public float m_enemyMove;
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		transform.Translate(Vector3.forward * m_enemyMove * Time.deltaTime);
	}
}
