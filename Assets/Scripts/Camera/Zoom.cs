using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {


	public float zoomMin = 1f;
	public float zoomMax = 6f;
	public float zoomAmount = 1f;
	Camera camara;
	// Use this for initialization
	void Awake() {

		camara = transform.GetComponentInChildren<Camera> ();
	}
	
	// Update is called once per frame

	void Update() {
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			//// ZOOM: in
			camara.orthographicSize = (camara.orthographicSize > zoomMin)
				? camara.orthographicSize - zoomAmount
				: zoomMin;
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			//// ZOOM: out
			camara.orthographicSize = (camara.orthographicSize < zoomMax)
				? camara.orthographicSize + zoomAmount
				: zoomMax;
		}
		;
	}
}
