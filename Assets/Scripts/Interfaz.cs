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
	public Image icon, healthBar, LowerProductionBar, UpperProductionBar;
	public Text nombre, ProductionDescription;
	public Transform PanelHabilidades, PanelRecursos, PanelSelections, PanelProduction;
	public GameObject genPanRecurso, genPanSelection;
	public Button DefaultButton;
	public List<Seleccionable> selecs = new List<Seleccionable>();
	public Jugador jugador;
	public BaseDatos baseDatos;
	public List<Text> textosRecursos = new List<Text>();
	public List<Image> healthBars = new List<Image>();
	public List<Image> productionIcons = new List<Image> ();

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
			if (selecs [0].objeto.type == 2 && selecs [0].productionQueue.Count > 0) {
				UpdateProductionBar ();
			}
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
				Seleccionable sel = selecs [i];
				GameObject panSelection = Instantiate (genPanSelection, PanelSelections);
				RectTransform rtrans = panSelection.transform as RectTransform;
				Objeto objeto = sel.objeto;
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
				panSelection.GetComponentInChildren<Button> ().onClick.AddListener(delegate {
					ChangeMainSelection(sel);
				});
			}
		}
	}
	public void CreateProductionPanels(){

		PanelProduction.gameObject.SetActive (true);
		UpdateProductionIcons ();

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
		if (image != null) {
			if (selecs [0].objeto.type == 1 || selecs [0].objeto.type == 2) {
				image.rectTransform.anchorMax = new Vector2 (sel.health / (sel.objeto as Ficha).health, image.rectTransform.anchorMax.y);
				image.rectTransform.offsetMax = new Vector2 (0, 0);
			}
		}
	}
	void UpdateHealthBars(){
		UpdateHealthBar (healthBar, selecs[0]);
		if (selecs.Count== healthBars.Count) {
			for (int i = 0; i < healthBars.Count; i++) {
				UpdateHealthBar (healthBars [i], selecs [i]);
			}
		}
	}

	void ChangeMainSelection(Seleccionable se){
		selecs [selecs.IndexOf (se)] = selecs [0];
		selecs [0] = se;
		for (int i = 1; i < selecs.Count; i++) {
			selecs [i].Invoke ("Deseleccion", 0.04f);
		}
		Invoke("UpdateSelection", 0.05f);
	}
	public void UpdateProductionBar(){
		UpperProductionBar.rectTransform.anchorMax = new Vector2 (selecs[0].productionAmount / selecs[0].maxProductionAmount, UpperProductionBar.rectTransform.anchorMax.y);
		UpperProductionBar.rectTransform.offsetMax = new Vector2 (0, 0);
	}
	public void UpdateProductionIcons(){
		if (selecs [0].productionQueue.Count > 0) {
			for (int i = 0; i < productionIcons.Count; i++) {
				if (i < selecs [0].productionQueue.Count) {
					productionIcons [i].sprite = selecs [0].productionQueue [i].objeto.icono;
				} else {
					productionIcons [i].sprite = null;
				}
			}
			UpdateProductionBar ();
			if (language == 0) {
				ProductionDescription.text = "Training...";
			} else if (language == 1) {
				ProductionDescription.text = "Entrenando...";
			}
		} else {
			PanelProduction.gameObject.SetActive (false);
		
		}
	
	}
	public void UpdateSelection(){
		PanelProduction.gameObject.SetActive (false);
		if (selecs.Count > 0) {
			if (selecs [0].objeto.type == 1) {
				CreateSelectionPanels ();
			} else if (selecs [0].objeto.type == 2 && selecs[0].productionQueue.Count>0) {
				CreateProductionPanels ();
			}		
			CreateHabilityButtons ();
		}
	}
}
