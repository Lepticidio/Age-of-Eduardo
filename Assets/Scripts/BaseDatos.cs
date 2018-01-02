using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDatos: MonoBehaviour {

	public List<Habilidad> habilidades = new List<Habilidad>();
	public List<Objeto> objetos = new List<Objeto>();

	void Awake () {
		Habilidad Build = new Habilidad ("Build");
		habilidades.Add (Build);
		Build.nombre[1] = "Construir";

		Unidad Citizen = new Unidad ("Citizen");
		objetos.Add (Citizen);
		Citizen.nombre[1] = "Ciudadano";
		Citizen.habilidades.Add (searchHabilidad ("Build"));


	}

	public Objeto searchObject (string nombre){

		Objeto resultado = objetos [0];

		for (int i = 0; i < objetos.Count; i++) {

			if(objetos[i].nombre[0] == nombre){
				
				resultado = objetos [i];
			}
		}
		return resultado;
	
	}

	public Habilidad searchHabilidad (string nombre){

		Habilidad resultado = habilidades [0];

		for (int i = 0; i < objetos.Count; i++) {

			if(habilidades[i].nombre[0] == nombre){

				resultado = habilidades [i];
			}
		}
		return resultado;

	}

}
