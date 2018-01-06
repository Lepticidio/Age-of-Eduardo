using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoleccion : Habilidad {

	public float Rate;
	Recurso recurso;

	public Recoleccion(string nombreEnglish, Recurso recur){
		type = 3;
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);
		recurso = recur;
	}
	public void Action(Seleccionable selec){

		selec.jugador.recursos [recurso.ID] += Rate;

	}
}
