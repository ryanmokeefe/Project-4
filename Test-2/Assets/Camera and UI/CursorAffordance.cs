using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]

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
	Vector2 cursorHotspot = new Vector2 (0, 0);



	CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
		
		// use CameraRayaster.cs
		cameraRaycaster = GetComponent<CameraRaycaster>();
		// register delegate calls
		cameraRaycaster.layerChangeObserver += OnLayerChanged;
	}
	
	// Update is called once per frame
	void OnLayerChanged (Layer newLayer) {

		print("Cursor is on new layer");

//		Vector3 mouse = Input.GetMouseButton(0);

		switch (newLayer) {
			// cursor for walk
		case Layer.Walkable:
			Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
			break;
		case Layer.Enemy:
			// cursor for enemy
			Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
			break;
		case Layer.RaycastEndStop:
			// cursor for unknown
			Cursor.SetCursor (unknownCursor, cursorHotspot, CursorMode.Auto);
			break;
		default: 
			Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
			Debug.LogError ("Cursor to show unknown");
			return;
		}
	}

	// TODO - consider deregistering OnLayerChanged on leaving all game scenes


}
