using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers;
using Assets.scripts.controllers.handlers;

public class Penguin : MonoBehaviour, Actionable<ControllableActions>, GameEntity {
    Dictionary<ControllableActions, Handler> actions = new Dictionary<ControllableActions, Handler>();

    private Vector3 destination;

	// Use this for initialization
	void Awake() {
	    InjectionRegister.Register(this);
	}

    void Update() {
        ExecuteAction(ControllableActions.Move);

        if (GetComponent<CharacterController>().velocity.magnitude < 0.2f) {
            ExecuteAction(ControllableActions.Stop);
        }
    }

    public void AddAction(ControllableActions actionName, Handler action) {
        actions.Add(actionName, action);
    }

    public void ExecuteAction(ControllableActions actionName) {
        actions[actionName].DoAction();
    }

    public string GetTag() {
        return TagConstants.PLAYER;
    }

    public void SetupComponents() {
        foreach (var handler in actions.Values) {
            handler.SetupComponents(gameObject);
        }
    }

    public Vector3 GetDestination() {
        return destination;
    }
}

