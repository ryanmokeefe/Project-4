using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

	// cursor imgs
	[SerializeField]
	Texture2D walkCursor = null;

	[SerializeField]
	Texture2D targetCursor = null;

	[SerializeField]
	Texture2D unknownCursor = null;

	[SerializeField]
	// fine tuning of where you click
	Vector2 cursorHotspot = new Vector2 (96, 96);



	CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
		
		// use CameraRayaster.cs
		cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void LateUpdate () {

		// print layer hit for each frame
//		print (cameraRaycaster.layerHit);
//		Vector3 mouse = Input.GetMouseButton(0);
		switch (cameraRaycaster.layerHit) {
			// cursor for walk
		case Layer.Walkable:
			Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
			break;
		case Layer.Enemy:
			// cursor for enemy
			Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
			break;
//		case Layer.RaycastEndStop:
//			// cursor for unknown
//			Cursor.SetCursor (unknownCursor, cursorHotspot, CursorMode.Auto);
//			break;
		default: 
			Debug.LogError ("Cursor to show unknown");
			return;
		}
	}
}


