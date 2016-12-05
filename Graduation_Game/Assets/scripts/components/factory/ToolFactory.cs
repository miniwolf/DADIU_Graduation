using Assets.scripts.controllers.actions.animation;
using Assets.scripts.controllers.actions.sound;
using Assets.scripts.controllers.handlers;
using Assets.scripts.sound;
using UnityEngine;

namespace Assets.scripts.components.factory
{
	public class ToolFactory {
		public static void BuildJump(Actionable<ToolActions> actionable, Animator animator) {
			actionable.AddAction(ToolActions.OnTrigger, CreateSpring(animator));
		}

		private static Handler CreateSpring(Animator animator) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.ToolSounds.JUMP_TRIGGERED));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.Tools.SPRING));
			return actionHandler;
		}
	}
}