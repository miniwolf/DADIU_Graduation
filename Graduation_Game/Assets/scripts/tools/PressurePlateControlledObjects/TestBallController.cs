using UnityEngine;
using System.Collections;
using Assets.scripts.components;

namespace Assets.scripts.tools.PressurePlateControlledObjects {
public class TestBallController : ObjectControlledByPressurePlate {

	public override void Trigger()	{
		Debug.Log ("Ball was Triggered!!!!!");
		}
	}
}
