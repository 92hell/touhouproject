using UnityEngine;
using System.Collections;

public class SimpleDisplay : MonoBehaviour {

	public CellGrid CellGrid; 
	void Update () { 
		if(Input.GetKeyDown(KeyCode.N)) { 
			CellGrid.EndTurn();
			//User ends his turn by pressing "n" on keyboard. 
		}
	}
}
