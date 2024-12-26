using System;

namespace RizeLibrary.StateMachine
{
	public class Decision
	{
		private readonly Func<bool> _decision;
		
		public Decision(Func<bool> decision)
		{
			_decision = decision;
		}
		
		/// <summary>
		/// 判定を行う
		/// </summary>
		public bool Decide()
		{
			return _decision.Invoke();
		}
	}
}
