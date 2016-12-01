using UnityEngine;

namespace Assets.scripts {
	public class AnimationSet {

		public string fallAnimation;
		public string jumpAnimation;
		public string slidingAnimation;
		public string celebrateAnimation;
		public string deathElectricAnimation;
		public string deathDrownAnimation;
		public string deathPitAnimation;
		public string deathSpikeWallAnimation;
		public string deathSpikeGroundAnimation;
		public string reactionToDeath;

	    public AnimationSet() {
	        fallAnimation = GetRandomAnimation(AnimationConstants.PENGUIN_FALL);
	        jumpAnimation = GetRandomAnimation(AnimationConstants.JUMP);
	        slidingAnimation = GetRandomAnimation(AnimationConstants.SLIDE);
	        celebrateAnimation = GetRandomAnimation(AnimationConstants.CELEBRATE);
	        deathElectricAnimation = GetRandomAnimation(AnimationConstants.ELECTRICUTION);
	        deathDrownAnimation = GetRandomAnimation(AnimationConstants.DROWNDEATH);
	        deathPitAnimation = GetRandomAnimation(AnimationConstants.PITDEATH);
	        deathSpikeWallAnimation = GetRandomAnimation(AnimationConstants.SPIKEDEATHWALL);
	        deathSpikeGroundAnimation = GetRandomAnimation(AnimationConstants.SPIKEDEATHGROUND);
	        reactionToDeath = GetRandomAnimation(AnimationConstants.TRIGGER_REACT_TO_DEATH);
	    }

	    private string GetRandomAnimation(string[] type) {
	        /*
                FieldInfo[] fields = typeof(AnimationConstants).GetFields().Where(f => f.GetRawConstantValue().ToString().StartsWith(type)).Cast<FieldInfo>().ToArray();
                return fields[UnityEngine.Random.Range(0, fields.Length)].GetRawConstantValue().ToString();
            */
	        return type[Random.Range(0, type.Length)];
	    }
	}
}
