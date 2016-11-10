﻿using System;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {

	public class SnappingTool : SnappingToolInterface {
		public float leftLaneOffset;
		public float rightLaneOffset;
		public int xSnapValue = 1;

		private RaycastHit hit;
		private const int layerMask = 1 << 8;
		private Vector3 pos;

		// Handles snapping on left or right lane
		public void Snap(Vector3 hitPos, Transform tool) {
			pos = Mathf.Abs(leftLaneOffset - hitPos.z) < Mathf.Abs(rightLaneOffset - hitPos.z)
					? new Vector3(hitPos.x, tool.position.y, leftLaneOffset)
					: new Vector3(hitPos.x, tool.position.y, rightLaneOffset);
			pos.x = Round(pos.x);

			Debug.DrawRay(new Vector3(pos.x, hitPos.y+1f, pos.z), -Vector3.up * 1f);
			if (Physics.Raycast(new Vector3(pos.x, hitPos.y+1f, pos.z), -Vector3.up, out hit, 2f, layerMask)) {
				tool.position = new Vector3(pos.x, hit.point.y, pos.z);
			}
		}

		private float Round(float input) {
			return xSnapValue * Mathf.Round(input / xSnapValue);
		}

		public void SetCenter (GameObject obj) {
			leftLaneOffset = obj.transform.position.z + 1f;
			rightLaneOffset = obj.transform.position.z - 1f;
			xSnapValue = obj.GetComponent<Settings>().GetXSnapValue();
		}
	}
}
