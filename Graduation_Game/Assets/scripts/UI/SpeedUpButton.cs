using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts;
using UnityEngine.UI;

public class SpeedUpButton : ActionableGameEntityImpl<GameActions> {
	private bool speedUp = false;
	private Color col;
	private ColorBlock cols;

	void Start(){
		cols = GetComponent<Button>().colors;
	}

	public void ExecuteSpeedUpAction(){
		if (!speedUp) {
			ExecuteAction(GameActions.SpeedUpPenguins);
			ColorUtility.TryParseHtmlString("#00FF6BFF",out col);
			cols.normalColor = col;
			GetComponent<Button>().colors = cols;
			GetComponent<Image>().color = col;
			speedUp = true;
		} else if (speedUp) {
			ExecuteAction(GameActions.ResetPenguinSpeed);
			ColorUtility.TryParseHtmlString("#FFFFFFFF",out col);
			cols.normalColor = col;
			GetComponent<Image>().color = col;
			GetComponent<Button>().colors = cols;
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
