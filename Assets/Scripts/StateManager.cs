using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	public enum m_states {SPLASH, MENU, PLAY, OVER};
	public GameObject[] m_gameStates;
	private GameObject m_ActiveState;
	
	
	// Use this for initialization
	void Start () {
		int NumStates = m_gameStates.Length;

		for(int i = 0; i < NumStates; i++) {
			m_gameStates[i].SetActive(false);
		}

		m_ActiveState = m_gameStates[0];
		m_ActiveState.SetActive(true);
	}
	
	public void PlayGame () {
		m_ActiveState.SetActive(false);
		m_ActiveState = m_gameStates[(int)m_states.PLAY];
		m_ActiveState.SetActive(true);
	}
}
