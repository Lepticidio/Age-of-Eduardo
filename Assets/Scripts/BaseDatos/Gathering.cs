using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gathering : Ability {

	public float Rate;
	Resource resource;

	public Gathering(string nombreEnglish, Resource recur){
		type = 3;
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);
		resource = recur;
	}
	public void Action(Seleccionable selec){

		selec.jugador.resources [resource.ID] += Rate;

	}
}
