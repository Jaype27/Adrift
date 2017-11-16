using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDestroy : MonoBehaviour {
	
	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "Boundary") {
			Destroy (this.gameObject);
		}
	}
}
