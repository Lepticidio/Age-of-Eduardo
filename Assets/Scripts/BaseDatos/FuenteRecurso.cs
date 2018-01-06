using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuenteRecurso : Objeto {

	public Recoleccion recoleccion;

	public FuenteRecurso(string nombreEnglish, Recoleccion recolec){
		recoleccion = recolec;
		nombre [0] = nombreEnglish;
		type = 4;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);

	}
}
