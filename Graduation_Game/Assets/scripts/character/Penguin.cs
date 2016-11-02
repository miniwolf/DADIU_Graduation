using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers;
using Assets.scripts.controllers.handlers;

public class Penguin : MonoBehaviour, Actionable<ControllableActions>, GameEntity, Directionable {
	public enum Lane {Left, Right};

    Dictionary<ControllableActions, Handler> actions = new Dictionary<ControllableActions, Handler>();
    private Vector3 direction;
	private Lane lane = Lane.Left;

	// Use this for initialization
	void Awake() {
	    InjectionRegister.Register(this);
	}

	void Start() {
		direction = new Vector3(1, 0, 0);
	}

    void Update() {
        ExecuteAction(ControllableActions.Move);
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

    public Vector3 GetDirection() {
        return direction;
    }

	public void SetDirection(Vector3 direction) {
		this.direction = direction;
	}

	public Lane GetLane() {
		return lane;
	}

	public void SetLane(Lane lane) {
		this.lane = lane;
	}
}

