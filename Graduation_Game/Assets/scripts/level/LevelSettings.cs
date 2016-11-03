﻿using UnityEngine;
using System.Collections;

namespace Assets.scripts.level {
	public class LevelSettings : MonoBehaviour {
		public float laneWidth = 2;

		public float GetLaneWidth() {
			return laneWidth;
		}
	}
}
