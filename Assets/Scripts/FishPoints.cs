using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoints : MonoBehaviour {

	public int pointsToAdd;
	
	// Only player picks up the object
	void OnTriggerEnter (Collider other) {
		
		if(other.GetComponent<PlayerControl>() == null)
			
			return;
	
	// When picked up, it adds points (or number)	
		GameManager.AddPoints(pointsToAdd);
	
	// Object disappears
		Destroy(gameObject);
	}
}
