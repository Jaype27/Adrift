using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProperties : MonoBehaviour {

	public Transform m_player;
	public float smoothTime = 0.15f;
	Vector3 velocity = Vector3.zero;

	public bool YmaxBool = false;
	public float YmaxValue = 0;
	public bool YminBool = false;
	public float YminValue = 0;
	public bool XmaxBool = false;
	public float XmaxValue = 0;
	public bool XminBool = false;
	public float XminValue = 0;
	
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 playerPos = m_player.position;

		
		if (YminBool && YmaxBool) 
			playerPos.y = Mathf.Clamp(m_player.position.y, YminValue, YmaxValue);
		
		else if (YminBool) 
			playerPos.y = Mathf.Clamp(m_player.position.y, YminValue, m_player.position.y);

		else if (YmaxBool)
			playerPos.y = Mathf.Clamp(m_player.position.y, m_player.position.y, YmaxValue);


		if (XminBool && XmaxBool)
			playerPos.x = Mathf.Clamp(m_player.position.x, XminValue, XmaxValue);
		
		else if (XminBool)
			playerPos.x = Mathf.Clamp(m_player.position.x, XminValue, m_player.position.x);

		else if (XmaxBool) 
			playerPos.x = Mathf.Clamp(m_player.position.x, m_player.position.x, XmaxValue);

		
		
		
		playerPos.z = transform.position.z;

		transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, smoothTime);


		

	}
}
