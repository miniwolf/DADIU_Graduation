using UnityEngine;
using System.Collections;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts;

public class Seal : ActionableGameEntityImpl<ControllableActions> {

	private bool isDead = false;
	private bool hasLanded = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			return;
		}else if(!hasLanded){
			return;
		}
		ExecuteAction(ControllableActions.SealMove);
	}


	public bool GetIsDead(){
		return isDead;
	}
	public void SetisDead(bool isDead){
		this.isDead = isDead;
	}

	public void SethasLanded(bool hasLanded){
		this.hasLanded = hasLanded;
	}

	public override string GetTag () {
		return TagConstants.SEAL;
	}
	public void SetDirection(Vector3 vec){
	}
}
