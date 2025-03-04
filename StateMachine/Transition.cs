namespace RizeLibrary.StateMachine
{
	public class Transition<TStateID>
	{
		private ICondition _conditions;
		private bool _conditionMet;
		
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
			_conditions = condition;
			_conditionMet = conditionMet;
		}
		
		public bool IsTriggered()
		{
			return _conditions.Decide() == _conditionMet;
		}
	}
	
	[System.Serializable]
	public class TransitionSettings
	{
		public bool HasExitTime = true;
		public float ExitTime = 0.75f;
	}
}
