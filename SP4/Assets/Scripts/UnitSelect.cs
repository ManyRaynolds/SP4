using UnityEngine;
using System.Collections;

public class UnitSelect : MonoBehaviour {

	public bool isSelected;

	private void OnSelected() {
		if (networkView.isMine) {
			isSelected = true;
			renderer.material.color = Color.green;
		}
	}

	private void OnUnselected() {
		if (networkView.isMine) {
			isSelected = false;
			renderer.material.color = Color.white;
		}
	}
}
