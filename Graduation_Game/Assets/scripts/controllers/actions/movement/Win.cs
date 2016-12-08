using Assets.scripts.components;
using Assets.scripts.sound;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement{
	public class Win : Action {
		private readonly Directionable direction;
		//private readonly Actionable<ControllableActions> actionable;

		public Win(Directionable direction, Actionable<ControllableActions> actionable){
			//this.actionable = actionable;
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			//characterController = gameObject.GetComponent<CharacterController>();
			//AkSoundEngine.PostEvent(SoundConstants.PenguinSounds.START_MOVING, gameObject);
		}

		public void Execute() {
			Vector3 newDir;
			var dir = direction.GetDirection();
			if (direction.GetLane() == Assets.scripts.character.Penguin.Lane.Left) {
				newDir = new Vector3(dir.x, dir.y,dir.z + GetRandRightLane());	
			} else {
				newDir = new Vector3(dir.x, dir.y,dir.z + GetRandLeftLane());
			}
			Vector3 normDir = Vector3.Normalize(newDir);
			direction.SetDirection(normDir);
		}

		float GetRandLeftLane(){
			return Random.Range(0f, 2f);
		}

		float GetRandRightLane(){
			return Random.Range(-2f, 0f);
		}
	}
}

