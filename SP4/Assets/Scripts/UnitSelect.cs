using UnityEngine;
using System.Collections;

public class UnitSelect : MonoBehaviour {

	public bool isSelected;

	private void OnSelected() {
		isSelected = true;
		renderer.material.color = Color.red;
		//Debug.Log("Hello", gameObject);
	}

	private void OnUnselected() {
		isSelected = false;
		renderer.material.color = Color.white;
	}
}
