using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource  {


	public string[] nombre = new string[2];
	public Sprite icono;
	public int ID;


	public Resource(string nombreEnglish, int id){

		ID = id;
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);

	}


}
