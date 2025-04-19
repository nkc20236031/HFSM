using RizeLibrary.StateMachine;
using UnityEngine;

public class ConditionIsDead : ICondition
{
	public void Initialize() { }

	public bool Decide()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}
}
