using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public Control control;
	public Objeto objeto;
	public BaseDatos baseDatos;
	public Seleccionable seleccionable;

	void Awake(){
		seleccionable = gameObject.GetComponent<Seleccionable> ();

	}
	// Use this for initialization
	void Start () {
		
		Invoke ("Actualizar", 0.1f);

	}

	void Actualizar(){
		objeto = seleccionable.objeto;
		baseDatos = seleccionable.baseDatos;
		control = baseDatos.gameObject.GetComponent<Control> ();
		for (int i = (int)(transform.position.x - objeto.ancho / 2); i < (int)(transform.position.x + objeto.ancho / 2); i++) {
			for (int j = (int)(transform.position.y - objeto.alto / 2); j < (int)(transform.position.y + objeto.alto / 2); j++) {
				control.grid [i, j].bloqueado = true;
			}
		}
		
	}

}
