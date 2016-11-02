using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.character {
public class Penguin : ActionableGameEntityImpl<ControllableActions>{
	public enum Lane {Left, Right};

    Dictionary<ControllableActions, Handler> actions = new Dictionary<ControllableActions, Handler>();
    private Vector3 direction;
	private Lane lane = Lane.Left;

	// Use this for initialization
	void Awake() {
	    InjectionRegister.Register(this);
	}

	void Start() {
		direction = Vector3.back;
	}

    void Update() {
        ExecuteAction(ControllableActions.Move);
    }

    public void AddAction(ControllableActions actionName, Handler action) {
        actions.Add(actionName, action);
    }

    public override string GetTag() {
        return TagConstants.PLAYER;
    }

    public Vector3 GetDestination() {
        return destination;
    }


    public Vector3 GetDirection() {
        return direction;
	}

    public void SetDestination(Vector3 destination) {
        this.destination = destination;
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

