using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.controllers.actions;
using Assets.scripts.character;
using Assets.scripts;
using Assets.scripts.level;

public class SpeedUpPenguins : Action {

	private List<Penguin> penguins = new List<Penguin>();
	private GameObject[] penguinSpawners;
	private float speedUpFactor;

	public void Setup (GameObject gameObject) {
		//this.penguins = penguins;
		penguinSpawners = GameObject.FindGameObjectsWithTag(TagConstants.PENGUIN_SPAWNER);
		speedUpFactor= penguinSpawners[0].GetComponent<PenguinSpawner>().GetSpeedUp();
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
			penguins[i].SetSpeedUp(true);
			penguins[i].SetWalkSpeed(penguins[i].GetWalkSpeed() * speedUpFactor);
		}
	}
}
