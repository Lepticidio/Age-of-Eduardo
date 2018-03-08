using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity  {
	
	//Tiene 3 clases hijas, Decoracion, FuenteRecurso y Ficha. Esta última tiene como hijas otras 2, Edificio y Unidad

	public int type = 0; // Indica si el objeto es una unidad, estructura o decoración
	public float size, alto, ancho;
	public string[] nombre = new string[2];

	public List<Gathering> gatherings = new List<Gathering>();
	public List<Active> activas = new List<Active>();
	public List<Creative> creatives = new List<Creative>();

	public Sprite icono;

}
