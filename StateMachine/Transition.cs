namespace RizeLibrary.StateMachine
{
	public class Transition<TStateID>
	{
		private ICondition _conditions;
		private bool _conditionMet;
		
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
			_conditions = condition;
			_conditionMet = conditionMet;
		}
		
		public bool IsTriggered()
		{
			return _conditions.Decide() == _conditionMet;
		}
	}
}
