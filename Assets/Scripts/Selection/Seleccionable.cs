using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyPathfinding;

public class Seleccionable : MonoBehaviour {

	/*
	 * CHARLA SERIA:
	 * Los ciudadanos no van a donde deberían cuando recogen recursos. 
	 * NearestFronterizo probablemente no funciona.
	 * */


	public bool selected, ocupado, recolectando;
	public Camera myCamera;
	public string nombre;
	public Objeto objeto;
	Selection selection;
	public BaseDatos baseDatos;
	Interfaz interfaz;
	GameObject botonDefecto, marcador;
	Pathfinding pathfinding;
	Control control;
	public List<Nodo> fronterizos = new List<Nodo>();
	Seleccionable fuente;

	public float altura;

	public Jugador jugador;

	// Use this for initialization
	void Awake () {
		
		myCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();		
		control = GameObject.Find ("Controlador").GetComponent<Control> ();
		selection = control.gameObject.GetComponent<Selection> ();
		baseDatos= control.gameObject.GetComponent<BaseDatos> ();
		interfaz= control.gameObject.GetComponent<Interfaz> ();
		marcador = Resources.Load<GameObject> ("MarcadorFresa");
	}

	void Start(){
		objeto = baseDatos.searchObject (nombre);
		if (objeto.type == 1) {
			pathfinding = gameObject.GetComponent<Pathfinding> ();
		}
		altura = objeto.alto;

	}

	void Update(){

		if (Mathf.Abs (myCamera.ScreenToWorldPoint (Input.mousePosition).x - transform.position.x) < objeto.ancho / 2 && Mathf.Abs (myCamera.ScreenToWorldPoint (Input.mousePosition).y - transform.position.y) < objeto.alto / 2) {
			control.puntero = this;
		} else if (control.puntero == this) {
			control.puntero = null;
		}
		if (Input.GetMouseButtonUp (0)&& selection.IsWithinSelectionBounds(gameObject)&& objeto.type ==1) {
			Debug.Log ("Es seleccionado");
			Seleccion ();
		}
		if (Input.GetMouseButtonDown (0)&& Input.mousePosition.y > myCamera.pixelHeight*interfaz.interfaceFraction) {
			if( !selected&& control.puntero ==this){
				Seleccion ();

			}
			else if (selected&&!ocupado) {
				Deseleccion();
			}
		}
		if (selected) {
			if (Input.GetMouseButtonDown (1)&&objeto.type == 1 &&  !ocupado) {
				if (control.puntero!= null&& control.puntero.objeto.type == 4&& objeto.recolecciones.Contains( (control.puntero.objeto as FuenteRecurso).recoleccion)) {
					fuente = control.puntero;
					fuente.GetFronterizos ();
					if (fuente.fronterizos.Count > 0) {
						Nodo des = fuente.NearestFronterizo (transform.position);
						pathfinding.destiny = new Vector3 (des.x, des.y,0);
						pathfinding.pathfinding ();
						recolectando = true;
					}
				} else {
					pathfinding.destiny = myCamera.ScreenToWorldPoint (Input.mousePosition);
					pathfinding.pathfinding ();
					recolectando = false;
				}
			}
		}
		if (recolectando && Vector3.Distance (fuente.transform.position, transform.position) < 4) {
			(fuente.objeto as FuenteRecurso).recoleccion.Action(this);
		}

	}

	void Seleccion(){
		
		selected = true;
		DrawCircle(objeto.size, 128, Color.red);
		interfaz.selecs.Add(this);
		interfaz.Invoke ("CreateHabilityButtons", 0.05f);

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
		for (int i = (int)(transform.position.x - objeto.ancho / 2); i < (int)(transform.position.x + objeto.ancho / 2)+2; i++) {
			for (int j = (int)(transform.position.y - objeto.alto / 2); j < (int)(transform.position.y + objeto.alto / 2)+2; j++) {
				if (!control.grid [i, j].bloqueado) {
					fronterizos.Add (control.grid [i, j]);
					//Instantiate(marcador, new Vector3( control.grid [i, j].x, control.grid [i, j].y), Quaternion.identity);
				}
			}
		}

	}

	public Nodo NearestFronterizo(Vector3 pos){

		Nodo resultado = fronterizos [0];
		for (int i = 1; i < fronterizos.Count; i++) {
			if (Vector3.SqrMagnitude (pos - new Vector3 (fronterizos [i].x, fronterizos [i].y, 0)) < Vector3.SqrMagnitude (pos - new Vector3 (resultado.x, resultado.y, 0))) {
				resultado = fronterizos [i];
			}
		}
		/*
		GameObject re = Instantiate(marcador, new Vector3( resultado.x, resultado.y), Quaternion.identity);
		re.GetComponent<SpriteRenderer> ().color = new Color (0, 1, 1, 0.25f);
		*/
		return resultado;
	}
}
