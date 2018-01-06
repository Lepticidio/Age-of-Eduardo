using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recurso  {

	//Tiene 3 clases hijas: Activa, Creativa y Recoleccion

	public string[] nombre = new string[2];
	public Sprite icono;
	public int ID;


	public Recurso(string nombreEnglish, int id){

		ID = id;
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);

	}


}
