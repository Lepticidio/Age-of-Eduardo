using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccionable : MonoBehaviour {

	public bool selected;
	public Camera myCamera;
	public string nombre;
	public Objeto objeto;
	Selection selection;
	BaseDatos baseDatos;
	Interfaz interfaz;
	GameObject botonDefecto;





	/* 
	 * CHARLA SERIA: 
	 * Tengo que mejorar la selección, ahora mismo la selección individual
	 * funciona en un 50% de los casos, y no parece que produzca los botones
	 * de habilidades.
	 * */








	// Use this for initialization
	void Awake () {
		
		myCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();		
		selection = GameObject.Find ("Controlador").GetComponent<Selection> ();		
		baseDatos= GameObject.Find ("Controlador").GetComponent<BaseDatos> ();
		interfaz= GameObject.Find ("Controlador").GetComponent<Interfaz> ();
	}

	void Start(){

		objeto = baseDatos.searchObject (nombre);
	}

	void Update(){
		
		if (Input.GetMouseButtonUp (0)&& selection.IsWithinSelectionBounds(gameObject)) {
			Seleccion ();
		}
		if (Input.GetMouseButtonDown (0)&& Input.mousePosition.y > myCamera.pixelHeight*interfaz.interfaceFraction) {
			if(  Mathf.Abs( myCamera.ScreenToWorldPoint (Input.mousePosition).x - transform.position.x )< objeto.ancho/2 && Mathf.Abs( myCamera.ScreenToWorldPoint (Input.mousePosition).y - transform.position.y )< objeto.alto/2 ){
				//Debug.Log ("Distancia" + Vector3.Distance( myCamera.ScreenToWorldPoint (Input.mousePosition) , transform.position).ToString());
				Seleccion ();

			}
			else {
				//Debug.Log("Dibujando por selección");
				Deseleccion();
			}
		}

	}

	void Seleccion(){
		
		selected = true;
		DrawCircle(objeto.size*1.2f, 128, Color.red);
		interfaz.nombre.text = objeto.nombre[interfaz.language];
		interfaz.icon.sprite = objeto.icono;
		if (objeto.type == 1) {
			interfaz.CreateHabilityButtons (objeto);
		}
	}

	void Deseleccion(){
		selected = false;
		StopDrawing ();
		interfaz.nombre.text = "";
		interfaz.icon.sprite = null;
		foreach (GameObject boton in GameObject.FindGameObjectsWithTag("Button")) {
			Destroy (boton);
		}
	}

	void DrawCircle ( float radius, int numSegments, Color c1) {
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.startColor =c1;
		lineRenderer.endColor = c1;
		lineRenderer.startWidth = 0.1f;
		lineRenderer.endWidth = 0.1f;
		lineRenderer.positionCount = numSegments + 1;
		lineRenderer.useWorldSpace = false;

		float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
		float theta = 0f;

		for (int i = 0 ; i < numSegments + 1 ; i++) {
			float x = radius * Mathf.Cos(theta);
			float y = radius * Mathf.Sin(theta);
			Vector3 pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
			theta += deltaTheta;



		}
	}


	void StopDrawing(){
		gameObject.GetComponent<LineRenderer> ().startColor = new Color (0, 0, 0, 0);
		gameObject.GetComponent<LineRenderer> ().endColor = new Color (0, 0, 0, 0);
	}
}
