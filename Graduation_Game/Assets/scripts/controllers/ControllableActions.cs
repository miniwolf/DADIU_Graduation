namespace Assets.scripts.controllers {
	public enum ControllableActions {
		SwitchLeft, SwitchRight,
		Move, Stop, Start,
		KillPenguinBySpikes, KillPenguinByPit,
		KillPenguinByExcavator,
		KillPenguingByWeightBased,
		StartJump, StopJump,
		StartSpeed, Speed, StopSpeed,
		StartEnlarge, Enlarge, StopEnlarge,
		StartMinimize, Minimize, StopMinimize,
		KillPenguinByElectricution,
		KillPenguinByOrca,
	    StartSliding, StopSliding,
	    Freeze, UnFreeze,
	    OtherPenguinDied, // reactions to death of other penguin
		SealJump, SealMove, SealFall, SealDeath, SealLand,
		Celebrate, Win
	}
}
