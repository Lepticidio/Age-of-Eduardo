using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDatos: MonoBehaviour {

	public List <Recurso> recursos = new List<Recurso> ();
	public List<Habilidad> habilidades = new List<Habilidad>();
	public List<Objeto> objetos = new List<Objeto>();
	void Awake () {


		Recurso Food= new Recurso ("Food", 0);
		recursos.Add (Food);
		Food.nombre[1] = "Comida";

		Recurso Wood= new Recurso ("Wood", 1);
		recursos.Add (Wood);
		Wood.nombre[1] = "Madera";

		Recoleccion Foraging = new Recoleccion ("Foraging", Food);
		habilidades.Add (Foraging);
		Foraging.nombre[1] = "Recolectar";
		Foraging.Rate = 0.01f;

		Recoleccion WoodCutting = new Recoleccion ("Wood Cutting", Wood);
		habilidades.Add (WoodCutting);
		WoodCutting.nombre[1] = "Talar Madera";
		WoodCutting.Rate = 0.02f;


		FuenteRecurso Tree = new FuenteRecurso ("Tree", WoodCutting);
		objetos.Add (Tree);
		Tree.size = 1;
		Tree.alto = 1;
		Tree.ancho = 1;
		Tree.nombre [1] = "Árbol";

		FuenteRecurso BerryBush = new FuenteRecurso ("Berry Bush", Foraging);
		objetos.Add (BerryBush);
		BerryBush.size = 2;
		BerryBush.alto = 3;
		BerryBush.ancho = 3;
		BerryBush.nombre [1] = "Arbusto de bayas";

		Decoration Rock = new Decoration ("Rock");
		objetos.Add (Rock);
		Rock.size = 5;
		Rock.alto = 5;
		Rock.ancho = 5;
		Rock.nombre [1] = "Roca";

		Unidad Citizen = new Unidad ("Citizen");
		objetos.Add (Citizen);
		Citizen.nombre[1] = "Ciudadano";
		Citizen.size = 2;
		Citizen.alto = 4;
		Citizen.ancho = 2;
		Citizen.recolecciones.Add (searchHabilidad ("Foraging") as Recoleccion);
		Citizen.recolecciones.Add (searchHabilidad ("Wood Cutting") as Recoleccion);

		Edificio TownCenter = new Edificio ("Town Center");
		objetos.Add (TownCenter);
		TownCenter.size = 7;
		TownCenter.alto = 14;
		TownCenter.ancho = 14;
		TownCenter.nombre[1] = "Centro Urbano";

		Creativa Build = new Creativa ("Build", TownCenter);
		habilidades.Add (Build);
		Build.nombre[1] = "Construir";
		Build.recursos.Add (Wood);
		Build.amounts.Add (50);

		Creativa CreateCitizen = new Creativa ("Create Citizen", Citizen);
		habilidades.Add (CreateCitizen);
		CreateCitizen.nombre[1] = "Crear Ciudadano";
		CreateCitizen.recursos.Add (Food);
		CreateCitizen.amounts.Add (50);

		Citizen.creativas.Add (searchHabilidad ("Build") as Creativa);
		TownCenter.creativas.Add (searchHabilidad ("Create Citizen") as Creativa);
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
