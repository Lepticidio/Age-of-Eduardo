using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {


	public float zoomMin = 1f;
	public float zoomMax = 6f;
	public float zoomAmount = 1f;
	Camera camera;
	// Use this for initialization
	void Awake() {

		camera = transform.GetComponentInChildren<Camera> ();
	}
	
	// Update is called once per frame

	void Update() {
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			//// ZOOM: in
			camera.orthographicSize = (camera.orthographicSize > zoomMin)
				? camera.orthographicSize - zoomAmount
				: zoomMin;
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			//// ZOOM: out
			camera.orthographicSize = (camera.orthographicSize < zoomMax)
				? camera.orthographicSize + zoomAmount
				: zoomMax;
		}
		;
	}
}
