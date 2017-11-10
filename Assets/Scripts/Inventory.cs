using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public GameObject[] inventory = new GameObject[2];
	
	public void AddItem (GameObject item) {
		
		bool itemAdded = false;
		
		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i] == null) {
				inventory[i] = item;
				Debug.Log(item.name + " was collected");
				itemAdded = true;
				item.SendMessage ("Collect");
				break;
			}
		}
	if(!itemAdded) {
		Debug.Log ("Inventory Full");
	}

	}
}
