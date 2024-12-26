using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class WalkAction : BaseAction
	{
		private readonly string _walkTextColor;
		private readonly Color _walkColor;
		private readonly Image _walkImage;
		
		public WalkAction(WalkStatus walkStatus)
		{
			_walkTextColor = walkStatus.TextColor;
			_walkColor = walkStatus.Color;
			_walkImage = walkStatus.Image;
			
			_walkImage.color = _walkColor;
		}

		public override void OnEnter()
		{
			Debug.Log($"<color={_walkTextColor}>Walk State</color>");
			
			_walkImage.gameObject.SetActive(true);
		}
		
		public override void OnExit()
		{
			_walkImage.gameObject.SetActive(false);
		}
	}
}
