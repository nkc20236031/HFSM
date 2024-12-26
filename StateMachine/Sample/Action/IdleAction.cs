using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class IdleAction : BaseAction
	{
		private readonly string _idleTextColor;
		private readonly Color _idleColor;
		private readonly Image _idleImage;
		
		public IdleAction(IdleStatus idleStatus)
		{
			_idleTextColor = idleStatus.TextColor;
			_idleColor = idleStatus.Color;
			_idleImage = idleStatus.Image;
			
			_idleImage.color = _idleColor;
		}
		
		public override void OnEnter()
		{
			Debug.Log($"<color={_idleTextColor}>Idle State</color>");
			
			_idleImage.gameObject.SetActive(true);
		}
		
		public override void OnExit()
		{
			_idleImage.gameObject.SetActive(false);
		}
	}
}
