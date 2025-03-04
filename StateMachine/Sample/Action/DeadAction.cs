using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class DeadAction : BaseAction
	{
		private readonly string _deadTextColor;
		private readonly Image _deadImage;
		
		public DeadAction(DeadStatus deadStatus)
		{
			_deadTextColor = deadStatus.TextColor;
			_deadImage = deadStatus.Image;
			
			Color deadColor = deadStatus.Color;
			_deadImage.color = deadColor;
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
