using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public Control control;
	public Entity entity;
	public Database database;
	public Seleccionable seleccionable;

	void Awake(){
		seleccionable = gameObject.GetComponent<Seleccionable> ();

	}
	// Use this for initialization
	void Start () {
		
		Invoke ("Actualizar", 0.1f);

	}

	void Actualizar(){
		entity = seleccionable.entity;
		database = seleccionable.database;
		control = database.gameObject.GetComponent<Control> ();
		for (int i =1 + (int)(transform.position.x - entity.ancho / 2); i < 1+(int)(transform.position.x + entity.ancho / 2); i++) {
			for (int j = 1+(int)(transform.position.y - entity.alto / 2); j < 1+(int)(transform.position.y + entity.alto / 2); j++) {
				control.grid [i, j].bloqueado = true;
			}
		}
		
	}

}
