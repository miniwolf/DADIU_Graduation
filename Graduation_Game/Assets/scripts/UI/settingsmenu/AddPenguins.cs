using UnityEngine;
using System.Collections;
using Assets.scripts.UI.inventory;

public class AddPenguins : MonoBehaviour {

	public void AddOnePenguins(){
		Inventory.penguinCount.SetValue(Inventory.penguinCount.GetValue()+1);
	}

	public void AddKeys(){
		Inventory.key.SetValue(Inventory.key.GetValue() + 1);
	}
}
