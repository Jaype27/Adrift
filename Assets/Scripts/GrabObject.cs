using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {

	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Wood") {
			other.transform.parent = transform;
			Debug.Log("GotLog");
		}
	}

	void OnCollisionExit (Collision other) {
		if(other.gameObject.tag == "Wood") {
			//other.transform.DetachChildren();
			other.transform.parent = null;
			Debug.Log("GettheLog");
		}	
	}
}
