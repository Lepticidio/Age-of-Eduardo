using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Habilidad {

	public string[] nombre = new string[2];

	public Sprite icono;

	public Habilidad(string nombreEnglish){
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);

	}


	public void Action(Seleccionable selec){
		if (nombre[0] == "Build") {
			Projection projection = MonoBehaviour.Instantiate (Resources.Load<GameObject>("Proyection")).GetComponent<Projection>();
			projection.edificio = projection.control.gameObject.GetComponent<BaseDatos> ().searchObject ("Town Center") as Edificio;
			projection.selec = selec;
			selec.ocupado = true;
			projection.Actualizar ();
		}
	}



}
