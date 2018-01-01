using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float offsetX, offsetY, downOffset, xSizeFactor = 1.8f, ySizeFactor = 0.98f, velocidad;

	public Camera camara;
	public Control control;

	// Use this for initialization
	void Awake() {

		camara = transform.GetComponentInChildren<Camera> ();
		control = GameObject.Find ("Controlador").GetComponent<Control> ();
	}

	
	// Update is called once per frame
	void Update () {

		if (transform.position.x < camara.orthographicSize*xSizeFactor) {
			transform.position = new Vector2( camara.orthographicSize*xSizeFactor, transform.position.y);
		}
		if (transform.position.y < camara.orthographicSize*(ySizeFactor+ downOffset)) {
			transform.position = new Vector2( transform.position.x, camara.orthographicSize*ySizeFactor +downOffset);
		}
		if (transform.position.x >  control.ancho -  camara.orthographicSize*xSizeFactor) {
			transform.position = new Vector2(  control.ancho -  camara.orthographicSize*xSizeFactor, transform.position.y);
		}
		if (transform.position.y >  control.alto -  camara.orthographicSize*ySizeFactor) {
			transform.position = new Vector2( transform.position.x ,control.alto -  camara.orthographicSize*ySizeFactor );
		}



		if (Input.mousePosition.x <offsetX && transform.position.x> camara.orthographicSize*xSizeFactor+ velocidad*camara.orthographicSize) {
			transform.position = new Vector2 (transform.position.x - velocidad*camara.orthographicSize,  transform.position.y);
		}
		if (Input.mousePosition.x > Screen.width - offsetX && transform.position.x<  control.ancho -  camara.orthographicSize*xSizeFactor - velocidad*camara.orthographicSize) {
			transform.position = new Vector2 (transform.position.x + velocidad*camara.orthographicSize,  transform.position.y);
		}
		if (Input.mousePosition.y <offsetY&& transform.position.y> camara.orthographicSize*(ySizeFactor+ velocidad+downOffset)) {
			transform.position = new Vector2 (transform.position.x ,  transform.position.y- velocidad*camara.orthographicSize);
		}
		if (Input.mousePosition.y > Screen.height - offsetY&& transform.position.y<  control.alto -  camara.orthographicSize*ySizeFactor - velocidad*camara.orthographicSize) {
			transform.position = new Vector2 (transform.position.x ,  transform.position.y+ velocidad*camara.orthographicSize);
		}



		
	}
}
