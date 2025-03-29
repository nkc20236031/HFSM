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
		/// 初期化を行う
		/// </summary>
		public void Initialize();
		
		/// <summary>
		/// 判定を行う
		/// </summary>
		public bool Decide()
		{
			return _condition.Invoke();
		}
	}
}
