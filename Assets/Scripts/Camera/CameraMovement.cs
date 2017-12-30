using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float offsetX, offsetY,xSizeFactor = 1.8f, ySizeFactor = 0.98f, velocidad;

	public Camera camera;
	public Control control;

	// Use this for initialization
	void Awake() {

		camera = transform.GetComponentInChildren<Camera> ();
		control = GameObject.Find ("Controlador").GetComponent<Control> ();
	}

	
	// Update is called once per frame
	void Update () {

		if (transform.position.x < camera.orthographicSize*xSizeFactor) {
			transform.position = new Vector2( camera.orthographicSize*xSizeFactor, transform.position.y);
		}
		if (transform.position.y < camera.orthographicSize*ySizeFactor) {
			transform.position = new Vector2( transform.position.x, camera.orthographicSize*ySizeFactor);
		}
		if (transform.position.x >  control.ancho -  camera.orthographicSize*xSizeFactor) {
			transform.position = new Vector2(  control.ancho -  camera.orthographicSize*xSizeFactor, transform.position.y);
		}
		if (transform.position.y >  control.alto -  camera.orthographicSize*ySizeFactor) {
			transform.position = new Vector2( transform.position.x ,control.alto -  camera.orthographicSize*ySizeFactor );
		}



		if (Input.mousePosition.x <offsetX && transform.position.x> camera.orthographicSize*xSizeFactor+ velocidad*camera.orthographicSize) {
			transform.position = new Vector2 (transform.position.x - velocidad*camera.orthographicSize,  transform.position.y);
		}
		if (Input.mousePosition.x > Screen.width - offsetX && transform.position.x<  control.ancho -  camera.orthographicSize*xSizeFactor - velocidad*camera.orthographicSize) {
			transform.position = new Vector2 (transform.position.x + velocidad*camera.orthographicSize,  transform.position.y);
		}
		if (Input.mousePosition.y <offsetY&& transform.position.y> camera.orthographicSize*ySizeFactor+ velocidad*camera.orthographicSize) {
			transform.position = new Vector2 (transform.position.x ,  transform.position.y- velocidad*camera.orthographicSize);
		}
		if (Input.mousePosition.y > Screen.height - offsetY&& transform.position.y<  control.alto -  camera.orthographicSize*ySizeFactor - velocidad*camera.orthographicSize) {
			transform.position = new Vector2 (transform.position.x ,  transform.position.y+ velocidad*camera.orthographicSize);
		}



		
	}
}
