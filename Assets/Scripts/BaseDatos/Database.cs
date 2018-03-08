using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database: MonoBehaviour {

	public List <Resource> resources = new List<Resource> ();
	public List<Ability> abilities = new List<Ability>();
	public List<Entity> entities = new List<Entity>();
	public List<Sprite> spritesTown = new List<Sprite>();
	void Awake () {

		Resource Food= new Resource ("Food", 0);
		resources.Add (Food);
		Food.nombre[1] = "Comida";

		Resource Wood= new Resource ("Wood", 1);
		resources.Add (Wood);
		Wood.nombre[1] = "Madera";

		Active Build = new Active ("Build");
		abilities.Add (Build);
		Build.nombre [1] = "Construir";
		Build.value = 0.02f;

		Gathering Foraging = new Gathering ("Foraging", Food);
		abilities.Add (Foraging);
		Foraging.nombre[1] = "Recolectar";
		Foraging.Rate = 0.01f;

		Gathering WoodCutting = new Gathering ("Wood Cutting", Wood);
		abilities.Add (WoodCutting);
		WoodCutting.nombre[1] = "Talar Madera";
		WoodCutting.Rate = 0.02f;


		ResourceSource Tree = new ResourceSource ("Tree", WoodCutting);
		entities.Add (Tree);
		Tree.size = 1;
		Tree.alto = 1;
		Tree.ancho = 1;
		Tree.nombre [1] = "Árbol";

		ResourceSource BerryBush = new ResourceSource ("Berry Bush", Foraging);
		entities.Add (BerryBush);
		BerryBush.size = 2;
		BerryBush.alto = 3;
		BerryBush.ancho = 3;
		BerryBush.nombre [1] = "Arbusto de bayas";

		Decoration Rock = new Decoration ("Rock");
		entities.Add (Rock);
		Rock.size = 5;
		Rock.alto = 5;
		Rock.ancho = 5;
		Rock.nombre [1] = "Roca";

		Unit Citizen = new Unit ("Citizen");
		entities.Add (Citizen);
		Citizen.health = 50;
		Citizen.size = 2;
		Citizen.alto = 4;
		Citizen.ancho = 2;
		Citizen.productionTime = 2;
		Citizen.gatherings.Add (Foraging);
		Citizen.gatherings.Add (WoodCutting);
		Citizen.activas.Add (Build);
		Citizen.nombre[1] = "Ciudadano";

		Unit Knight = new Unit ("Knight");
		entities.Add (Knight);
		Knight.health = 120;
		Knight.size = 2;
		Knight.alto = 4;
		Knight.ancho = 2;
		Knight.productionTime = 4;
		Knight.nombre[1] = "Caballero";

		Building TownCenter = new Building ("Town Center", 5);
		entities.Add (TownCenter);
		TownCenter.health = 500;
		TownCenter.size = 7;
		TownCenter.alto = 14;
		TownCenter.ancho = 14;
		TownCenter.productionTime = 50;
		TownCenter.productionRate = 0.02f;
		TownCenter.nombre[1] = "Centro Urbano";


		Creative BuildTownCenter = new Creative ("Build Town Center", TownCenter);
		abilities.Add (BuildTownCenter);
		BuildTownCenter.nombre[1] = "Construir Centro Urbano";
		BuildTownCenter.resources.Add (Wood);
		BuildTownCenter.amounts.Add (50);

		Creative CreateCitizen = new Creative ("Create Citizen", Citizen);
		abilities.Add (CreateCitizen);
		CreateCitizen.nombre[1] = "Crear Ciudadano";
		CreateCitizen.resources.Add (Food);
		CreateCitizen.amounts.Add (50);

		Citizen.creatives.Add (BuildTownCenter);
		TownCenter.creatives.Add (CreateCitizen);
	}

	public Entity searchObject (string nombre){

		Entity resultado = entities [0];

		for (int i = 0; i < entities.Count; i++) {

			if(entities[i].nombre[0] == nombre){
				
				resultado = entities [i];
			}
		}
		return resultado;
	
	}

	public Ability searchAbility (string nombre){

		Ability resultado = abilities [0];

		for (int i = 0; i < abilities.Count; i++) {

			if(abilities[i].nombre[0] == nombre){

				resultado = abilities [i];
			}
		}
		return resultado;

	}

}
