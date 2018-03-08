using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour {

	public List<float> resources = new List<float>();
	Database database;


	void Awake(){	
		database = GameObject.Find ("Controlador").GetComponent<Database> ();
	}

	void Start(){

		for (int i = 0; i < database.resources.Count; i++) {
			resources.Add (0f);
		}

	}
}
