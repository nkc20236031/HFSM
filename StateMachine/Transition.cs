using System.Collections.Generic;

namespace RizeLibrary.StateMachine
{
	public class Transition<TStateID>
	{
		private readonly Dictionary<ICondition, bool> _conditions = new();
		
		public TStateID FromStateID { get; }
		public TStateID ToStateID { get; }

		public Transition(TStateID fromStateID, TStateID toStateID)
		{
			FromStateID = fromStateID;
			ToStateID = toStateID;
		}
		
		/// <summary>
		/// 条件の追加
		/// </summary>
		/// <param name="condition">条件</param>
		/// <param name="conditionMet">条件を満たすか</param>
		public void AddCondition(ICondition condition, bool conditionMet)
		{
			_conditions.Add(condition, conditionMet);
		}
		
		/// <summary>
		/// 遷移条件の初期化
		/// </summary>
		public void ConditionInitialize()
		{
			foreach (KeyValuePair<ICondition, bool> condition in _conditions)
			{
				// 条件の初期化
				condition.Key.Initialize();
			}
		}
		
		/// <summary>
		/// すべての条件を満たしているか
		/// </summary>
		public bool IsTriggered()
		{
			foreach (KeyValuePair<ICondition, bool> condition in _conditions)
			{
				if (!condition.Key.Decide() == condition.Value) { return false; }
			}

			return true;
		}
	}

	public class AnyTransition<TStateID> : Transition<TStateID>
	{
		public AnyTransition(TStateID toStateID) : base(default, toStateID) { }
	}
}
