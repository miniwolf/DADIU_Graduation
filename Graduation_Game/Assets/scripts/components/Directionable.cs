using UnityEngine;
using Assets.scripts.character;

namespace Assets.scripts.components {
	public interface Directionable {
		Vector3 GetDirection();
		void SetDirection(Vector3 direction);
		float GetJumpSpeed();
		float GetWalkSpeed();
		void SetSpeed(float speed);
		float GetSpeed();
		void SetScale(Vector3 scale);
		Vector3 GetInitialScale();
		void SetJump(bool jump);
		bool GetJump();
		void SetCurve(Penguin.CurveType type, AnimationCurve curve);
		void SetInitialTime(Penguin.CurveType type, float time);
		AnimationCurve GetCurve(Penguin.CurveType type);
		float GetInitialTime(Penguin.CurveType type);
		void removeCurve(Penguin.CurveType type);
		void removeInitialTime(Penguin.CurveType type);
		bool IsRunning();
		void SetRunning(bool running);
		bool IsEnlarging();
		void SetEnlarging(bool enlarging);
		bool IsMinimizing();
		void SetMinimizing(bool minimizing);
		Penguin.Weight GetWeight();
		void SetWeight(Penguin.Weight weight);

		float GetGroundY();
	}
}
