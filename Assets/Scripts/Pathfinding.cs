using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyPathfinding;

public class Pathfinding : MonoBehaviour {

	public int ancho, alto, contaPuntos = 0;
	public float speed;
	Vector3 targetPosition;
	public Camera myCamera;
	public Nodo[,] grid;
	public Control control;
	public List<Nodo> currentPath = new List<Nodo>();
	void Awake(){
		myCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		control = GameObject.Find ("Controlador").GetComponent<Control> ();

	}
	// Use this for initialization
	void Start () {
		ancho = control.ancho;
		alto = control.alto;
		grid = new Nodo[ancho, alto];
		for (int i = 0; i < ancho; i++) {
			for (int j = 0; j < alto; j++) {
				Nodo nodo = new Nodo ();
				grid [i, j] = nodo;
				nodo.x = i;
				nodo.y = j;
			}
		}
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		// esto hay que quitarlo luego, tiene que ser utilizado por ia también
		if (Input.GetButtonDown ("Fire2")){
			pathfinding (myCamera.ScreenToWorldPoint (Input.mousePosition));
		}

		if (targetPosition != transform.position) {
			
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed);
		} else {
			if ( contaPuntos< currentPath.Count-1) {
				Debug.Log ("llega");
				contaPuntos++;
				targetPosition = GetPosition (currentPath [contaPuntos]);

			}
		}

	}

	Vector3 GetPosition (Nodo nodo){
		 
		return new Vector3 (nodo.x, nodo.y, 0);

	}

	Nodo GetNodo(Vector3 position){

		return grid[(int)(position.x ), (int)(position.y)];
			
	}



	public void pathfinding(Vector3 destiny){
		contaPuntos = 0;
		for (int i = 0; i < ancho; i++) {
			for (int j = 0; j < alto; j++) {
				grid [i, j].G = 9999999;
				grid [i, j].comprobado = false;
				grid [i, j].parent = null;
				if (control.grid [i, j].bloqueado == true) {
					grid [i, j].bloqueado = true;
				} else {
					grid [i, j].bloqueado = false;
				}
			}
		}
		List<Nodo> origen = new List<Nodo> ();
		origen.Add (GetNodo (transform.position));
		origen [0].G = 0;
		Nodo destino = GetNodo(destiny);
		iterator ( origen ,destino,0);
		currentPath = GetPath(destino);
		currentPath.Reverse ();
		if (currentPath.Count > 0) {
			targetPosition = GetPosition (currentPath [0]);
		}
	}
	void iterator (List<Nodo> originales, Nodo destino, int contador){
		List<Nodo> lista = new List<Nodo> ();
		bool encontrado = false;
		for (int i = 0; i < originales.Count; i++) {
			List<Nodo> adyacentes = Adyacentes (originales [i]);
			for (int j = 0; j < adyacentes.Count; j++) {
				if (!adyacentes [j].comprobado) {
					if (destino == adyacentes [j]) {
						encontrado = true;
					}
					lista.Add (adyacentes [j]);
					adyacentes [j].comprobado = true;
				}
			}
		}
		if (!encontrado&& contador< 100) {
			contador++;
			iterator (lista, destino, contador);
		}
	}


	List<Nodo> Adyacentes(Nodo origen){
		List<Nodo> lista = new List<Nodo> ();
		if (origen.y < alto - 1) {
			Nodo nodo = grid [origen.x, origen.y + 1];
			if (!nodo.comprobado &&!nodo.bloqueado) {
				lista.Add (nodo);
				if (nodo.G > origen.G + 10) {
					nodo.G = origen.G + 10;
					nodo.parent = origen;
				}
			}
		}
		if (origen.y > 0) {
			Nodo nodo = grid [origen.x, origen.y - 1];
			if (!nodo.comprobado&&!nodo.bloqueado) {
				lista.Add (nodo);
				if (nodo.G > origen.G + 10) {
					nodo.G = origen.G + 10;
					nodo.parent = origen;
				}
			}
		}
		if (origen.x < ancho - 1) {
			Nodo nodo = grid [origen.x+1, origen.y];
			if (!nodo.comprobado&&!nodo.bloqueado) {
				lista.Add (nodo);
				if (nodo.G > origen.G + 10) {
					nodo.G = origen.G + 10;
					nodo.parent = origen;
				}
			}
		}
		if (origen.x > 0) {
			Nodo nodo = grid [origen.x -1, origen.y];
			if (!nodo.comprobado&&!nodo.bloqueado) {
				lista.Add (nodo);
				if (nodo.G > origen.G + 10) {
					nodo.G = origen.G + 10;
					nodo.parent = origen;
				}
			}
		}
		if (origen.y < alto - 1 && origen.x < ancho - 1) {
			Nodo nodo = grid [origen.x+1, origen.y + 1];
			if (!nodo.comprobado&&!nodo.bloqueado) {
				lista.Add (nodo);
				if (nodo.G > origen.G + 14) {
					nodo.G = origen.G + 14;
					nodo.parent = origen;
				}
			}
		}
		if (origen.y > 0 && origen.x > 0) {
			Nodo nodo = grid [origen.x-1, origen.y - 1];
			if (!nodo.comprobado&&!nodo.bloqueado) {
				lista.Add (nodo);
				if (nodo.G > origen.G + 14) {
					nodo.G = origen.G + 14;
					nodo.parent = origen;
				}
			}
		}
		if (origen.y > 0 &&origen.x < ancho - 1) {
			Nodo nodo = grid [origen.x+1, origen.y-1];
			if (!nodo.comprobado&&!nodo.bloqueado){
				lista.Add (nodo);
				if (nodo.G > origen.G + 14) {
					nodo.G = origen.G + 14;
					nodo.parent = origen;
				}
			}
		}
		if (origen.y < alto - 1 &&origen.x > 0) {
			Nodo nodo =  grid [origen.x -1, origen.y+1];
			if (!nodo.comprobado&&!nodo.bloqueado) {
				lista.Add (nodo);
				if (nodo.G > origen.G + 14) {
					nodo.G = origen.G + 14;
					nodo.parent = origen;
				}
			}
		}
		return lista;
	}


	List<Nodo> GetPath(Nodo destino){
		List<Nodo> lista = new List<Nodo> ();
		AddToPath (destino, ref lista);
		return lista;
	}

	void AddToPath(Nodo hijo,ref List<Nodo> path){
		if (hijo.parent!= null){
			path.Add (hijo.parent);
			AddToPath (hijo.parent, ref path);
		}
	}
}
