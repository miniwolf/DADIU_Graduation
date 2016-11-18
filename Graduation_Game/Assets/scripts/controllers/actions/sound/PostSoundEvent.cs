using UnityEngine;

namespace Assets.scripts.controllers.actions.sound {
	public class PostSoundEvent : Action {
		private readonly string soundEvent;
		private GameObject go;

		public PostSoundEvent(string soundEvent) {
			this.soundEvent = soundEvent;
		}

		public void Setup(GameObject gameObject) {
			go = gameObject;
		}

		public void Execute() {
			AkSoundEngine.PostEvent(soundEvent, go);
		}
	}
}
