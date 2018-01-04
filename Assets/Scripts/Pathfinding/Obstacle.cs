using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public string nombre;
	public Control control;
	public Objeto objeto;
	public BaseDatos baseDatos;

	void Awake(){
		control = GameObject.Find ("Controlador").GetComponent<Control> ();
		baseDatos = GameObject.Find ("Controlador").GetComponent<BaseDatos> ();

	}
	// Use this for initialization
	void Start () {
		objeto = baseDatos.searchObject (nombre);
		for (int i = (int)(transform.position.x - objeto.ancho / 2); i < (int)(transform.position.x + objeto.ancho / 2); i++) {
			for (int j = (int)(transform.position.y - objeto.alto / 2); j <(int)(transform.position.y + objeto.alto / 2); j++) {
				control.grid [i,j].bloqueado = true;
			}
		}

	}

}
