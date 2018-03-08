using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creative : Ability {

	GameObject projectionGO, unitGO;
	public Entity entity;
	public List<float> amounts = new List<float>();
	public List<Resource> resources = new List<Resource>();

	public Creative(string nombreEnglish, Entity obj){
		entity = obj;
		type = 2;
		nombre [0] = nombreEnglish;
		icono = obj.icono;
		projectionGO = Resources.Load<GameObject> ("Projection");
		unitGO = Resources.Load<GameObject> ("Unit");
	}
	public void Action(Seleccionable selec){
		if (ResourcesRequeridos (selec.jugador)) {
			if (nombre [0] == "Build Town Center") {
				Projection projection = MonoBehaviour.Instantiate (projectionGO).GetComponent<Projection> ();
				projection.building = entity as Building;
				projection.selec = selec;
				projection.creative = this;
				projection.jugador = selec.jugador;
				selec.ocupado = true;
				projection.Actualizar ();
			} else if (nombre [0] == "Create Citizen") {
				selec.productionQueue.Add (this);
				selec.interfaz.CreateProductionPanels ();
				SpendResources (selec);
				if (selec.productionQueue.Count == 1) {
					selec.maxProductionAmount = (entity as Token).productionTime;
				}
			}
		}
	}
	public void Work(Seleccionable selec){
		Token token = selec.entity as Token;
		if (selec.productionAmount < selec.maxProductionAmount - token.productionRate) {
			selec.productionAmount += token.productionRate;

		} else {
			selec.GetFronterizos ();
			if (selec.fronterizos.Count > 0) {
				Seleccionable sele = MonoBehaviour.Instantiate (unitGO, new Vector3 (selec.fronterizos [0].x, selec.fronterizos [0].y, 0), Quaternion.identity).GetComponent<Seleccionable>();
				sele.jugador = selec.jugador;
				selec.productionQueue.RemoveAt (0);
				selec.productionAmount = 0;
				if (selec.productionQueue.Count > 0) {
					selec.maxProductionAmount = (selec.productionQueue[0].entity as Token).productionTime;
				}
				selec.interfaz.UpdateProductionIcons ();
			}
		}
	}
	public bool ResourcesRequeridos(Jugador jug){
		bool resultado = true;
		for (int i = 0; i < resources.Count; i++) {
			if (jug.resources [resources [i].ID] < amounts [i]) {
				resultado = false;
			}
		}
		return resultado;
	}
	public void SpendResources(Seleccionable selec){
		for (int i = 0; i < resources.Count; i++) {
			selec.jugador.resources [resources [i].ID] -= amounts [i];
		}
	}
}
