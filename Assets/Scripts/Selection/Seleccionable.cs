using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyPathfinding;

public class Seleccionable : MonoBehaviour {

	public bool selected, ocupado;
	public Camera myCamera;
	public string nombre;
	public Objeto objeto;
	Selection selection;
	public BaseDatos baseDatos;
	Interfaz interfaz;
	GameObject botonDefecto;
	Pathfinding pathfinding;
	Control control;
	public List<Nodo> fronterizos = new List<Nodo>();

	public float altura;


	// Use this for initialization
	void Awake () {
		
		myCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();		
		control = GameObject.Find ("Controlador").GetComponent<Control> ();
		selection = control.gameObject.GetComponent<Selection> ();
		baseDatos= control.gameObject.GetComponent<BaseDatos> ();
		interfaz= control.gameObject.GetComponent<Interfaz> ();
	}

	void Start(){
		objeto = baseDatos.searchObject (nombre);
		if (objeto.type == 1) {
			pathfinding = gameObject.GetComponent<Pathfinding> ();
		}
		altura = objeto.alto;

	}

	void Update(){
		
		if (Input.GetMouseButtonUp (0)&& selection.IsWithinSelectionBounds(gameObject)&& objeto.type ==1) {
			Seleccion ();
		}
		if (Input.GetMouseButtonDown (0)&& Input.mousePosition.y > myCamera.pixelHeight*interfaz.interfaceFraction) {
			if( !selected&& Mathf.Abs( myCamera.ScreenToWorldPoint (Input.mousePosition).x - transform.position.x )< objeto.ancho/2 && Mathf.Abs( myCamera.ScreenToWorldPoint (Input.mousePosition).y - transform.position.y )< objeto.alto/2 ){
				//Debug.Log ("Distancia" + Vector3.Distance( myCamera.ScreenToWorldPoint (Input.mousePosition) , transform.position).ToString());
				Seleccion ();

			}
			else if (selected&&!ocupado) {
				Deseleccion();
			}
		}
		if (Input.GetMouseButtonDown (1)&&selected && objeto.type == 1 &&  !ocupado) {
			pathfinding.destiny = myCamera.ScreenToWorldPoint (Input.mousePosition);
			pathfinding.pathfinding ();
		}

	}

	void Seleccion(){
		
		selected = true;
		DrawCircle(objeto.size, 128, Color.red);
		interfaz.nombre.text = objeto.nombre[interfaz.language];
		interfaz.icon.sprite = objeto.icono;
		interfaz.selecs.Add(this);
		interfaz.CreateHabilityButtons ();

	}

	void Deseleccion(){
		selected = false;
		StopDrawing ();
		interfaz.selecs.Remove (this);
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

	public void GetFronterizos(){
		fronterizos.Clear ();
		for (int i = (int)(transform.position.x - objeto.ancho / 2)-1; i < (int)(transform.position.x + objeto.ancho / 2)+1; i++) {
			for (int j = (int)(transform.position.y - objeto.alto / 2)-1; j < (int)(transform.position.y + objeto.alto / 2)+1; j++) {
				if (!control.grid [i, j].bloqueado) {
					fronterizos.Add (control.grid [i, j]);
				}
			}
		}

	}
}
