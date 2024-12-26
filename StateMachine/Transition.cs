namespace RizeLibrary.StateMachine
{
	public class Transition<TStateID>
	{
		public TStateID FromStateID { get; }
		public TStateID ToStateID { get; }
		public Decision Decision { get; }
		public bool ConditionMet { get; }

		public Transition(TStateID fromStateID, TStateID toStateID, Decision decision, bool conditionMet)
		{
			FromStateID = fromStateID;
			ToStateID = toStateID;
			Decision = decision;
			ConditionMet = conditionMet;
		}
		
		public bool IsTriggered()
		{
			return Decision.Decide() == ConditionMet;
		}
	}
}
