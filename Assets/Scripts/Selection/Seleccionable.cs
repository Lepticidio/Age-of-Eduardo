using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccionable : MonoBehaviour {

	public bool selected;
	public Camera myCamera;
	Selection selection;
	public float size;
	// Use this for initialization
	void Awake () {
		
		myCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();		
		selection = GameObject.Find ("Controlador").GetComponent<Selection> ();

	}


	void Update(){

		if (Input.GetMouseButtonUp (0)&& selection.IsWithinSelectionBounds(gameObject)) {
			selected = true;
			DrawCircle(size*2, 128, Color.red);
		}
		if (Input.GetMouseButtonDown (0)) {
			if( ! (Vector3.Distance( myCamera.ScreenToWorldPoint (Input.mousePosition) , transform.position)< size/2)){
			selected = false;
			StopDrawing ();
			}
			else {
				selected = true;
				DrawCircle(size*2, 128, Color.red);
			}
		}

	}

	void DrawCircle ( float radius, int numSegments, Color c1) {
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c1);
		lineRenderer.SetWidth(0.1f, 0.1f);
		lineRenderer.SetVertexCount(numSegments + 1);
		lineRenderer.useWorldSpace = false;

		float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
		float theta = 0f;

		for (int i = 0 ; i < numSegments + 1 ; i++) {
			float x = radius * Mathf.Cos(theta);
			float y = radius * Mathf.Sin(theta);
			Vector3 pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
			theta += deltaTheta;
		}
	}


	void StopDrawing(){
		gameObject.GetComponent<LineRenderer> ().startColor = new Color (0, 0, 0, 0);
		gameObject.GetComponent<LineRenderer> ().endColor = new Color (0, 0, 0, 0);
	}
}
