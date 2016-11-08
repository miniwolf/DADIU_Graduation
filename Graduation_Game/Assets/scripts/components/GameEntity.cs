using UnityEngine;

namespace Assets.scripts.components {
	public interface GameEntity  {
		string GetTag();
		void SetupComponents();
		GameObject GetGameObject();
		Actionable<T> GetActionable<T>();
	}
}
