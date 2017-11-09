using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour {

	public bool inventory;
	public float itemsCollected = 0;

	public void Collect () {
		gameObject.SetActive(false);
		Debug.Log("Collected");
		itemsCollected++;
		Debug.Log(itemsCollected);
	}
}
