using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyPathfinding;

public class Seleccionable : MonoBehaviour {
	
	public bool selected, ocupado, recolectando, construyendo, construido = true;
	public float health, buildAmount, maxBuildAmount, productionAmount, maxProductionAmount;
	public string nombre;
	public Camera myCamera;
	GameObject botonDefecto, marcador;
	public Entity entity;
	public Database database;
	Selection selection;
	public Interfaz interfaz;
	Pathfinding pathfinding;
	Control control;
	Seleccionable fuente, cimientos;
	public List<Nodo> fronterizos = new List<Nodo>();
	public List<Creative> productionQueue = new List<Creative> ();

	public float altura;

	public Jugador jugador;



	/* 
	 * CHARLA SERIA:
	 * Los ciudadanos creados en el lado derecho del centro urbano no se mueven
	 * por alguna razón desconocida.
	 * 
	 * UPDATE:
	 * Los ciudadanos también deben de ir al nodo más cercano de uno ocupado, 
	 * no ir hacia allí hasta que no puedan seguir y se paren, formando colas.
	 * 
	 * UPDATE:
	 * Que no se haga selección grupal e individual al mismo tiempo.
	 * 
	 * */




	// Use this for initialization
	void Awake () {
		
		myCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();		
		control = GameObject.Find ("Controlador").GetComponent<Control> ();
		selection = control.gameObject.GetComponent<Selection> ();
		database= control.gameObject.GetComponent<Database> ();
		interfaz= control.gameObject.GetComponent<Interfaz> ();
		marcador = Resources.Load<GameObject> ("MarcadorFresa");
	}

	void Start(){
		entity = database.searchObject (nombre);
		altura = entity.alto;
		if (entity.type == 1 || entity.type == 2) {
			health = (entity as Token).health;
			if (entity.type == 1) {
				pathfinding = gameObject.GetComponent<Pathfinding> ();
				for (int i = 1 + (int)(transform.position.x - entity.ancho / 2); i < 1 + (int)(transform.position.x + entity.ancho / 2); i++) {
					for (int j = 1 + (int)(transform.position.y - entity.alto / 2); j < 1 + (int)(transform.position.y + entity.alto / 2); j++) {
						control.grid [i, j].bloqueado = true;
					}
				}
			}
		}
	}

	void Update(){

		if (Mathf.Abs (myCamera.ScreenToWorldPoint (Input.mousePosition).x - transform.position.x) < entity.ancho / 2 && Mathf.Abs (myCamera.ScreenToWorldPoint (Input.mousePosition).y - transform.position.y) < entity.alto / 2) {
			control.puntero = this;
		} else if (control.puntero == this) {
			control.puntero = null;
		}
		if (Input.GetMouseButtonUp (0)&& selection.IsWithinSelectionBounds(gameObject)&& entity.type ==1) {
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
			if (Input.GetMouseButtonDown (1) && entity.type == 1 ) {
				Destinar ();
			}

		}
		if (productionQueue.Count>0) {
			productionQueue [0].Work (this);
		}
		if (recolectando && Vector3.Distance (fuente.transform.position, transform.position) < fuente.entity.size*2) {
			(fuente.entity as ResourceSource).gathering.Action (this);
		}
		if (construyendo && Vector3.Distance (cimientos.transform.position, transform.position) < cimientos.entity.size*2) {
			if (!cimientos.construido) {
				Active activa = database.searchAbility ("Build") as Active;
				activa.Action (cimientos);
			} else {
				construyendo = false;
			}
		}
	}

	public void Destinar(){
		
		if (control.puntero!= null&& control.puntero.entity.type == 4&& entity.gatherings.Contains( (control.puntero.entity as ResourceSource).gathering)&& !ocupado) {
			fuente = control.puntero;
			fuente.GetFronterizos ();
			if (fuente.fronterizos.Count > 0) {
				Nodo des = fuente.NearestFronterizo (transform.position);
				pathfinding.destiny = new Vector3 (des.x, des.y,0);
				pathfinding.pathfinding ();
				recolectando = true;
			}
		}else if (control.puntero!= null&& control.puntero.entity.type == 2&& entity.creatives.Contains( database.searchAbility("Build Town Center") as Creative)) {
			cimientos = control.puntero;
			cimientos.GetFronterizos ();
			if (cimientos.fronterizos.Count > 0) {
				Nodo des = cimientos.NearestFronterizo (transform.position);
				pathfinding.destiny = new Vector3 (des.x, des.y,0);
				pathfinding.pathfinding ();
				construyendo = true;
			}
		} else if(!ocupado) {
			pathfinding.destiny = myCamera.ScreenToWorldPoint (Input.mousePosition);
			pathfinding.pathfinding ();
			recolectando = false;
			construyendo = false;
		}
	}


	void Seleccion(){
		
		selected = true;
		DrawCircle(entity.size, 128, Color.red);
		interfaz.selecs.Add(this);
		interfaz.Invoke ("UpdateSelection", 0.04f);

	}

	public void Deseleccion(){
		selected = false;
		StopDrawing ();
		interfaz.selecs.Remove (this);
		interfaz.UpdateSelection ();
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
		for (int i = (int)(transform.position.x - entity.ancho / 2); i < (int)(transform.position.x + entity.ancho / 2)+2; i++) {
			for (int j = (int)(transform.position.y - entity.alto / 2); j < (int)(transform.position.y + entity.alto / 2)+2; j++) {
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
