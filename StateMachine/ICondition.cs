namespace RizeLibrary.StateMachine
{
	public interface ICondition
	{
		/// <summary>
		/// 判定を行う
		/// </summary>
		public bool Decide();
	}
}
