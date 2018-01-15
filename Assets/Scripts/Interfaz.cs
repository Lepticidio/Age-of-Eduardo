using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour {

	bool iniciada;
	public bool cleared = true;
	public int language;
	public float interfaceFraction, sizeButtonX, sizeButtonY, margenX, margenY, sizePanRecursoX, sizePanRecursoY, margenXRecurso, margenYRecurso, 
		sizePanSelectionX, sizePanSelectionY, margenXSelection, margenYSelection;
	public Image icon, healthBar;
	public Text nombre;
	public Transform PanelHabilidades, PanelRecursos, PanelSelections;
	public GameObject genPanRecurso, genPanSelection;
	public Button DefaultButton;
	public List<Seleccionable> selecs = new List<Seleccionable>();
	public Jugador jugador;
	public BaseDatos baseDatos;
	public List<Text> textosRecursos = new List<Text>();
	public List<Image> healthBars = new List<Image>();

	/* 
	 * CHARLA SERIA: 
	 * Para la selección múltiple tengo que ordenar los selecs por "prioridad".
	 * Esta será mayor en unidades especiales con habilidades, como ciudadanos,
	 * y entre las unidades normales cuanto más fuerte sea más "prioridad".	 * 
	 * 
	 *
	 * */

	void Awake(){
		baseDatos= gameObject.GetComponent<BaseDatos> ();
	}

	void Start(){

		CreateResourcePanels ();
	}

	void Update(){
		ActualizarRecursos ();
		if (!cleared && selecs.Count == 0) {
			ClearButtons ();
		}
		if (selecs.Count > 0) {
			UpdateHealthBars ();
		}
	}




	public void CreateHabilityButtons(){
		nombre.text = selecs[0].objeto.nombre[language];
		icon.sprite = selecs[0].objeto.icono;
		float xMax = sizeButtonX;
		float xMin= margenX;
		float yMax = margenY;
		float yMin = margenY - sizeButtonY;
		for (int i = 0; i < selecs[0].objeto.creativas.Count; i++) {
			Creativa creativa = selecs[0].objeto.creativas [i];
			Button boton = (Button)Instantiate (DefaultButton,PanelHabilidades);
			RectTransform rtrans = boton.transform as RectTransform;
			rtrans.anchorMax = new Vector2(xMax, yMax);
			rtrans.anchorMin = new Vector2(xMin, yMin);
			rtrans.offsetMax = new Vector2(0,0);
			rtrans.offsetMin = new Vector2(0,0);
			xMax+= sizeButtonX;
			xMin+= sizeButtonX;
			if (xMax > 1) {
				xMax = sizeButtonX;
				xMin= margenX;
				yMax -= sizeButtonY;
				yMin -= sizeButtonY;
			
			}
			boton.image.sprite = creativa.icono;
			boton.onClick.AddListener(delegate{creativa.Action(selecs[0]);});


		}
		for (int i = 0; i < selecs[0].objeto.activas.Count; i++) {
			Activa activa = selecs[0].objeto.activas [i];
			Button boton = (Button)Instantiate (DefaultButton,PanelHabilidades);
			RectTransform rtrans = boton.transform as RectTransform;
			rtrans.anchorMax = new Vector2(xMax, yMax);
			rtrans.anchorMin = new Vector2(xMin, yMin);
			rtrans.offsetMax = new Vector2(0,0);
			rtrans.offsetMin = new Vector2(0,0);
			xMax+= sizeButtonX;
			xMin+= sizeButtonX;
			if (xMax > 1) {
				xMax = sizeButtonX;
				xMin= margenX;
				yMax -= sizeButtonY;
				yMin -= sizeButtonY;

			}
			boton.image.sprite = activa.icono;
			boton.onClick.AddListener(delegate{activa.Action(selecs[0]);});


		}
		cleared = false;

	}

	public void CreateResourcePanels(){		
		float xMax = sizePanRecursoX;
		float xMin= margenXRecurso;
		float yMax = margenYRecurso;
		float yMin = margenYRecurso - sizePanRecursoY;
		for (int i = 0; i < jugador.recursos.Count; i++) {
			GameObject panRecurso = Instantiate (genPanRecurso,PanelRecursos);
			RectTransform rtrans = panRecurso.transform as RectTransform;
			Recurso recurso = baseDatos.recursos [i];
			rtrans.anchorMax = new Vector2(xMax, yMax);
			rtrans.anchorMin = new Vector2(xMin, yMin);
			rtrans.offsetMax = new Vector2(0,0);
			rtrans.offsetMin = new Vector2(0,0);
			xMax+= sizePanRecursoX;
			xMin+= sizePanRecursoX;
			if (xMax > 1) {
				xMax = sizePanRecursoX;
				xMin= margenXRecurso;
				yMax -= sizePanRecursoY;
				yMin -= sizePanRecursoY;

			}
			panRecurso.GetComponentsInChildren<Image>()[1].sprite = recurso.icono;
			Text textoRecurso = panRecurso.GetComponentInChildren<Text> ();
			textoRecurso.text = ((int)jugador.recursos[i]).ToString();
			textosRecursos.Add (textoRecurso);
		}
	}
	public void CreateSelectionPanels(){
		ClearButtons ();
		healthBars.Clear ();
		if (selecs.Count > 0) {
			float xMax = sizePanSelectionX;
			float xMin = margenXSelection;
			float yMax = margenYSelection;
			float yMin = margenYSelection - sizePanSelectionY;
			for (int i = 0; i < selecs.Count; i++) {
				GameObject panSelection = Instantiate (genPanSelection, PanelSelections);
				RectTransform rtrans = panSelection.transform as RectTransform;
				Objeto objeto = selecs [i].objeto;
				rtrans.anchorMax = new Vector2 (xMax, yMax);
				rtrans.anchorMin = new Vector2 (xMin, yMin);
				rtrans.offsetMax = new Vector2 (0, 0);
				rtrans.offsetMin = new Vector2 (0, 0);
				xMax += sizePanSelectionX;
				xMin += sizePanSelectionX;
				if (xMax > 1) {
					xMax = sizePanSelectionX;
					xMin = margenXSelection;
					yMax -= sizePanSelectionY;
					yMin -= sizePanSelectionY;

				}
				panSelection.GetComponentsInChildren<Image> () [1].sprite = objeto.icono;
				healthBars.Add (rtrans.GetChild (1).GetChild (1).GetComponent<Image>());
			}
		}
	}
	public void ClearButtons(){
		nombre.text = "";
		icon.sprite = null;
		foreach (GameObject boton in GameObject.FindGameObjectsWithTag("Button")) {
			Destroy (boton);
		}
		cleared = true;
	}
	void ActualizarRecursos(){
		for (int i = 0; i< textosRecursos.Count; i++) {
			textosRecursos[i].text = ((int)jugador.recursos[i]).ToString();
		}
	}
	void UpdateHealthBar(Image image ,Seleccionable sel){
		if (selecs [0].objeto.type == 1 || selecs [0].objeto.type == 2) {
			image.rectTransform.anchorMax = new Vector2( sel.health / (sel.objeto as Ficha).health,image.rectTransform.anchorMax.y);
			image.rectTransform.offsetMax = new Vector2 (0, 0);
		}
	}
	void UpdateHealthBars(){
		UpdateHealthBar (healthBar, selecs[0]);
		for (int i = 0; i < healthBars.Count; i++) {
			UpdateHealthBar (healthBars [i], selecs [i]);
		}
	}
}
