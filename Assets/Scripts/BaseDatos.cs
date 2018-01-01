using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDatos: MonoBehaviour {

	public List<Habilidad> habilidades = new List<Habilidad>();
	public List<Objeto> objetos = new List<Objeto>();

	void Awake () {
		Habilidad Build = new Habilidad ("Build");
		habilidades.Add (Build);
		Build.nombreSpanish = "Construir";

		Unidad Citizen = new Unidad ("Citizen");
		objetos.Add (Citizen);
		Citizen.nombreSpanish = "Ciudadano";


	}

	public Objeto search(string nombre){

		Objeto resultado = objetos [0];

		for (int i = 0; i < objetos.Count; i++) {

			if(objetos[i].nombreEnglish == nombre){
				
				resultado = objetos [i];
			}
		}
		return resultado;
	
	}
}
