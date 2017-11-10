using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspect : MonoBehaviour {

	public GameObject m_inspect = null;
	public CollectItem currentInspectObject = null;
	public Inventory inventory;

	void Update () {
		if(Input.GetButtonDown ("Inspect") && m_inspect) {
			if(currentInspectObject.inventory) {
				inventory.AddItem (m_inspect);
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if(other.CompareTag("Resource")) {
			Debug.Log("Found Item");
			m_inspect = other.gameObject;
			currentInspectObject = m_inspect.GetComponent<CollectItem>();
		}
	}

	void OnTriggerExit (Collider other) {
		if(other.CompareTag("Resource")) {
			Debug.Log("Away from Item");
			m_inspect = null;
		}
	}
}
