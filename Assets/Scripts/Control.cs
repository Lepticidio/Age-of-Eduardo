using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyPathfinding;

public class Control : MonoBehaviour {

	public int ancho, alto;
	public Nodo[,] grid;
	public Seleccionable puntero;
	public Jugador jugador;
	public List<Jugador> jugadores = new List<Jugador>();

	public GameObject edificio;
	// Use this for initialization
	void Awake () {
		grid = new Nodo[ancho, alto];
		for (int i = 0; i < ancho; i++) {
			for (int j = 0; j < alto; j++) {
				Nodo nodo = new Nodo ();
				grid [i, j] = nodo;
				nodo.x = i;
				nodo.y = j;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.X)) {
			for (int i = 0; i< jugador.recursos.Count; i++) {
				jugador.recursos[i] += 10000;
			}
		}
	}
}
