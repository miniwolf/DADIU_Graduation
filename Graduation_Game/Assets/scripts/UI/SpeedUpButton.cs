using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts;

public class SpeedUpButton : ActionableGameEntityImpl<GameActions> {
	private bool speedUp = false;

	public void ExecuteSpeedUpAction(){
		if (!speedUp) {
			ExecuteAction(GameActions.SpeedUpPenguins);
			speedUp = true;
		} else if (speedUp) {
			ExecuteAction(GameActions.ResetPenguinSpeed);
			speedUp = false;
		}
	}
	/*
	public void ExecuteResetSpeedUpAction(){
		ExecuteAction(GameActions.ResetPenguinSpeed);
	}
*/
	public override string GetTag () {
		return TagConstants.UI.PENGUINSPEEDUPBUTTON;
	}
}
