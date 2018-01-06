using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto  {
	
	//Tiene 3 clases hijas, Decoracion, FuenteRecurso y Ficha. Esta última tiene como hijas otras 2, Edificio y Unidad

	public int type = 0; // Indica si el objeto es una unidad, estructura o decoración
	public float size, alto, ancho, vida;
	public string[] nombre = new string[2];

	public List<Recoleccion> recolecciones = new List<Recoleccion>();
	public List<Activa> activas = new List<Activa>();
	public List<Creativa> creativas = new List<Creativa>();

	public Sprite icono;

}
