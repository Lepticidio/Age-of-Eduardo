using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccionable : MonoBehaviour {

	public bool selected;
	Selection selection;

	// Use this for initialization
	void Awake () {
		
		selection = GameObject.Find ("Controlador").GetComponent<Selection> ();

	}


	void Update(){


		if (Input.GetMouseButtonUp (0)&& selection.IsWithinSelectionBounds(gameObject)) {
			selected = true;
			gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
		}
		if (Input.GetMouseButtonDown (0)) {
			selected = false;
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
			
		}
	}

}
