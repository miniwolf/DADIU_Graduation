using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.controllers.actions;
using Assets.scripts.character;
using Assets.scripts;
using Assets.scripts.level;

public class ResetPenguinSpeed : Action {

	private List<Penguin> penguins = new List<Penguin>();
	private float origSpeed = 0.6f;
	private GameObject[] penguinSpawners;

	public void Setup (GameObject gameObject) {
		origSpeed = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_TEMPLATE).GetComponent<Penguin>().GetWalkSpeed();
		penguinSpawners = GameObject.FindGameObjectsWithTag(TagConstants.PENGUIN_SPAWNER);
	}

	public void Execute () {
		for (int j = 0; j < penguinSpawners.Length; j++) {
			List<GameObject> penguinsGO = penguinSpawners[j].GetComponent<PenguinSpawner>().GetAllPenguins();
			for (int i = 0; i < penguinsGO.Count; i++) {
				penguins.Add(penguinsGO[i].GetComponent<Penguin>());
			}
		}
		for (int i = 0; i < penguins.Count; i++) {
			if (penguins[i].IsDead()) {
				continue;
			}
			if (!penguins[i].GetJump()) {
				penguins[i].SetSpeedUp(false);
				penguins[i].SetSpeed(origSpeed);
				penguins[i].SetWalkSpeed(origSpeed);
			}
		}
	}
}
