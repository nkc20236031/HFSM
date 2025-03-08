using System.Collections.Generic;

namespace RizeLibrary.StateMachine
{
	public class Transition<TStateID>
	{
		private readonly Dictionary<ICondition, bool> _conditions = new();
		
		public TStateID FromStateID { get; }
		public TStateID ToStateID { get; }
		public TransitionSettings Settings { get; }

		public Transition(TStateID fromStateID, TStateID toStateID, TransitionSettings settings = null)
		{
			FromStateID = fromStateID;
			ToStateID = toStateID;
			Settings = settings ?? new TransitionSettings();
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
		public AnyTransition(TStateID toStateID, TransitionSettings settings = null) : base(default, toStateID, settings) { }
	}

	[System.Serializable]
	public class TransitionSettings
	{
		public bool HasExitTime = true;
		public float ExitTime = 0.75f;
	}
}
