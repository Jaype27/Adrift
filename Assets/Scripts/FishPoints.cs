using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoints : MonoBehaviour {

	public float pointsToAdd;
	public float addOxygen;
	public float numLife;
	private PlayerControl playerOxygen;
	private GameManager addLives;

	void Awake () {
		playerOxygen = FindObjectOfType<PlayerControl>();
		addLives = FindObjectOfType<GameManager>();
	}
	
	// Only player picks up the object
	void OnTriggerEnter (Collider other) {
		
		if(other.GetComponent<PlayerControl>() == null)
			return;
		GameManager.AddPoints(pointsToAdd);
		Destroy(gameObject);

		if(playerOxygen.m_currentOxygen < playerOxygen.m_maxOxygen) {
			playerOxygen.m_currentOxygen = playerOxygen.m_currentOxygen + addOxygen;
			Destroy(gameObject);
		}

		if (addLives.m_lives < 3) {
			addLives.m_lives = addLives.m_lives + numLife;
			Destroy(gameObject);
		} else if (addLives.m_lives >= 3) {
			Destroy(gameObject);
		}

	}


}
