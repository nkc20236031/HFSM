namespace RizeLibrary.StateMachine
{
	public interface ICondition
	{
		/// <summary>
		/// 初期化を行う
		/// </summary>
		public void Initialize();
		
		/// <summary>
		/// 判定を行う
		/// </summary>
		public bool Decide();
	}
}
