using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour {

	public bool inventory;
	public void Collect () {
		gameObject.SetActive(false);
		Debug.Log("Collected");
	}
}
