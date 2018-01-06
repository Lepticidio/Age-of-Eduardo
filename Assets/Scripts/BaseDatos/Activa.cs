using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activa : Habilidad {



	public Activa(string nombreEnglish){
		type = 1;
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);
	}
	public void Action(Seleccionable selec){

	}
}
