﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {


	public bool isSelecting = false;
	Vector3 mousePosition1;

	void Update()
	{
		// If we press the left mouse button, save mouse location and begin selection
		if (Input.GetMouseButtonDown (0)) {
			isSelecting = true;
			mousePosition1 = Input.mousePosition;
		}
		// If we let go of the left mouse button, end selection
		if (Input.GetMouseButtonUp (0))
			isSelecting = false;
	}

	void OnGUI()
	{
		if (isSelecting) {
			// Create a rect from both mouse positions
			var rect = Utils.GetScreenRect (mousePosition1, Input.mousePosition);
			Utils.DrawScreenRect (rect, new Color (0.8f, 0.8f, 0.95f, 0.1f));
			Utils.DrawScreenRectBorder (rect, 2, new Color (0.8f, 0.8f, 0.95f));
		}
	}
	public bool IsWithinSelectionBounds( GameObject gameObject )
	{
		var camera = Camera.main;
		var viewportBounds = Utils.GetViewportBounds (camera, mousePosition1, Input.mousePosition);
		if (viewportBounds.Contains (camera.WorldToViewportPoint (gameObject.transform.position))) {
			Debug.Log ("Lo contiene");
		}
		return viewportBounds.Contains (camera.WorldToViewportPoint (gameObject.transform.position));

	}
}

