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
		/// ステートマシンが実行されたときに呼び出される初期化処理
		/// </summary>
		public virtual void Initialize() { }
		
		/// <summary>
		/// 判定を行う
		/// </summary>
		public bool Decide()
		{
			return _condition.Invoke();
		}
	}
}
