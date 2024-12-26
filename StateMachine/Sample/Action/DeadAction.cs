using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class DeadAction : BaseAction
	{
		private readonly string _deadTextColor;
		private readonly Color _deadColor;
		private readonly Image _deadImage;
		
		public DeadAction(DeadStatus deadStatus)
		{
			_deadTextColor = deadStatus.TextColor;
			_deadColor = deadStatus.Color;
			_deadImage = deadStatus.Image;
			
			_deadImage.color = _deadColor;
		}
		
		public override void OnEnter()
		{
			Debug.Log($"<color={_deadTextColor}>Dead State</color>");
			
			_deadImage.gameObject.SetActive(true);
		}
		
		public override void OnExit()
		{
			_deadImage.gameObject.SetActive(false);
		}
	}
}
