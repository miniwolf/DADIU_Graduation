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
			System.Random rnd = new System.Random();
			//int change = rnd.Next(-10,10);
			int range = 10;
			double rDouble;
			do {
				rDouble = rnd.NextDouble()* range;
				Debug.Log(rDouble);
			} while(rDouble - 5 < 2 && rDouble - 5 > -2);
			var dir = direction.GetDirection();
			Vector3 newDir = new Vector3(dir.x, dir.y,dir.z + (float)rDouble-5);
			Vector3 normDir = Vector3.Normalize(newDir);
			direction.SetDirection(normDir);

		}
	}
}

