using RizeLibrary.StateMachine;
using UnityEngine;

public class ConditionIsDead : ICondition
{
	public bool Decide()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}
}
