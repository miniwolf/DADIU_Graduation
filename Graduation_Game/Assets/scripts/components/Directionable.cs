using UnityEngine;
using Assets.scripts.character;

namespace Assets.scripts.components {
	public interface Directionable {
		Vector3 GetDirection();
		void SetDirection(Vector3 direction);
		float GetJumpSpeed();
		float GetWalkSpeed();
		float GetSlideSpeedupIncrement();
		float GetSlideMaxSpeedMult();
		void SetSpeed(float speed);
		float GetSpeed();
		void SetScale(Vector3 scale);
		Vector3 GetInitialScale();
		void SetJump(bool jump);
		void SetSlide(bool jump);
		bool GetJump();

		bool GetDoubleJump ();

		bool GetSpeedUp();

		void SetDoubleJump (bool b);

		Assets.scripts.character.Penguin.Lane GetLane ();

		void SetCurve(Penguin.CurveType type, AnimationCurve curve);
		AnimationCurve GetCurve(Penguin.CurveType type);
		void SetInitialTime(Penguin.CurveType type, float time);
		float GetInitialTime(Penguin.CurveType type);

		void removeCurve(Penguin.CurveType type);
		void removeInitialTime(Penguin.CurveType type);
		bool IsRunning();
		bool IsSliding();
		void SetRunning(bool running);
		bool IsEnlarging();
		void SetEnlarging(bool enlarging);
		bool IsMinimizing();
		void SetMinimizing(bool minimizing);
		Penguin.Weight GetWeight();
		void SetWeight(Penguin.Weight weight);

		float GetGroundY();
		void SetGoingTo(Penguin.Lane left);
	}
}
