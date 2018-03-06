using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

	// create a radius for player to stop BEFORE hitting target dest. (prevents player from spinning in circles/standing on lookTarget)
	[SerializeField]
	float walkMoveStopRadius = 0.2f;

	// A reference to the ThirdPersonCharacter on the object
    ThirdPersonCharacter m_Character;   
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
	bool isInDirectMode = false; 


    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

//	private void Update()
//	{
//		if (!m_Jump)
//		{
//			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
//		}
//	}


	// FIX: click to move and WASD conflict

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
		// TODO not toggling
		// allow player to remap G later
		if (Input.GetKeyDown (KeyCode.G)) { 
			isInDirectMode = !isInDirectMode;
			// clear last clickTarget position
			currentClickTarget = transform.position;
		}
		if (isInDirectMode) {
			// process direct movement
		
		} else {
			// Mouse movement
			ProcessMouseMovement (); 
		}


    }


	private void ProcessDirectMovement() {

		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		bool crouch = Input.GetKey(KeyCode.C);

		// calculate camera relative direction to move:
		Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

		m_Character.Move (m_Move, false, false);

	}

	void ProcessMouseMovement() {
	
		if (Input.GetMouseButton(0))
		{
			// TODO - move mouse movement from TPC


			// print("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString());
			print("Cursor raycast hit" + cameraRaycaster.layerHit);

			switch (cameraRaycaster.layerHit) 
			{
			case Layer.Walkable:
				currentClickTarget = cameraRaycaster.hit.point;  // So not set in default case
				break;
			case Layer.Enemy:
				print ("not moving to enemy");
				break;
			default: 
				//				print ("Unexpected Layer Found");
				return;
			}

			//			currentClickTarget = cameraRaycaster.hit.point;  // So not set in default case

		}
		var playerToClickPoint = currentClickTarget - transform.position;
		if (playerToClickPoint.magnitude >= walkMoveStopRadius) {
			// move in the direction of the difference of where you are/where you want to go; jump=false, crouch=false
			//			m_Character.Move (currentClickTarget - transform.position, false, false);
			m_Character.Move (playerToClickPoint, false, false);
		} else {
			m_Character.Move (Vector3.zero, false, false);


		}
	
	}

}

