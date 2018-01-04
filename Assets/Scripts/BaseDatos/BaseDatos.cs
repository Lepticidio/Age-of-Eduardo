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

		Habilidad CreateCitizen = new Habilidad ("Create Citizen");
		habilidades.Add (CreateCitizen);
		Build.nombre[1] = "Crear Ciudadano";

		Decoration Rock = new Decoration ("Rock");
		objetos.Add (Rock);
		Rock.size = 5;
		Rock.alto = 5;
		Rock.ancho = 5;


		Unidad Citizen = new Unidad ("Citizen");
		objetos.Add (Citizen);
		Citizen.nombre[1] = "Ciudadano";
		Citizen.size = 2;
		Citizen.alto = 4;
		Citizen.ancho = 2;
		Citizen.habilidades.Add (searchHabilidad ("Build"));

		Edificio TownCenter = new Edificio ("Town Center");
		objetos.Add (TownCenter);
		TownCenter.size = 14;
		TownCenter.alto = 14;
		TownCenter.ancho = 14;
		TownCenter.nombre[1] = "Centro Urbano";
		TownCenter.habilidades.Add (searchHabilidad ("Create Citizen"));


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

		for (int i = 0; i < habilidades.Count; i++) {

			if(habilidades[i].nombre[0] == nombre){

				resultado = habilidades [i];
			}
		}
		return resultado;

	}

}
