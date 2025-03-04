namespace RizeLibrary.StateMachine
{
	public abstract class BaseAction
	{
		/// <summary>
		/// 状態が開始されるときに呼び出される。
		/// </summary>
		public virtual void OnEnter() { }
		
		/// <summary>
		/// 固定フレームレートで状態が更新されるときに呼び出される。
		/// </summary>
		public virtual void OnFixedUpdate() { }
		
		/// <summary>
		/// 状態が更新されるときに呼び出される。
		/// </summary>
		public virtual void OnUpdate() { }
		
		/// <summary>
		/// 状態が終了するときに呼び出される。
		/// </summary>
		public virtual void OnExit() { }
	}
}
