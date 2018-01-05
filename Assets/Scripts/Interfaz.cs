using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour {

	public bool cleared = true;
	public int language;
	public float sizeButtonX, sizeButtonY, margenX, margenY, interfaceFraction;
	public Image icon;
	public Text nombre;
	public string idioma;
	public Transform PanelHabilidades;
	public Button DefaultButton;
	public List<Seleccionable> selecs = new List<Seleccionable>();

	/* 
	 * CHARLA SERIA: 
	 * Para la selección múltiple tengo que ordenar los selecs por "prioridad".
	 * Esta será mayor en unidades especiales con habilidades, como ciudadanos,
	 * y entre las unidades normales cuanto más fuerte sea más "prioridad".	 * 
	 * 
	 * */


	void Update(){
		if (!cleared && selecs.Count == 0) {
			ClearButtons ();
		}
	}

	public void CreateHabilityButtons(){
		float xMax = sizeButtonX;
		float xMin= margenX;
		float yMax = margenY;
		float yMin = margenY - sizeButtonY;
		for (int i = 0; i < selecs[0].objeto.habilidades.Count; i++) {
			Habilidad habilidad = selecs[0].objeto.habilidades [i];
			Button boton = (Button)Instantiate (DefaultButton,PanelHabilidades);
			RectTransform rtrans = boton.transform as RectTransform;
			rtrans.anchorMax = new Vector2(xMax, yMax);
			rtrans.anchorMin = new Vector2(xMin, yMin);
			rtrans.offsetMax = new Vector2(0,0);
			rtrans.offsetMin = new Vector2(0,0);
			xMax-= sizeButtonX;
			xMin-= sizeButtonX;
			if (xMax > 1) {
				xMax = sizeButtonX;
				xMin= margenX;
				yMax -= sizeButtonY;
				yMin -= sizeButtonY;
			
			}
			boton.image.sprite = habilidad.icono;
			boton.onClick.AddListener(delegate{habilidad.Action(selecs[0]);});


		}
		cleared = false;

	}

	public void ClearButtons(){
		nombre.text = "";
		icon.sprite = null;
		foreach (GameObject boton in GameObject.FindGameObjectsWithTag("Button")) {
			Destroy (boton);
		}
		cleared = true;
	}
}
