using System;

namespace RizeLibrary.StateMachine
{
	public class Condition : ICondition
	{
		private readonly Func<bool> _condition;
		
		public Condition(Func<bool> condition)
		{
			_condition = condition;
		}
		
		/// <summary>
		/// 判定を行う
		/// </summary>
		public bool Decide()
		{
			return _condition.Invoke();
		}
	}
}
