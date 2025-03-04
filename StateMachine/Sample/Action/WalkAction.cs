using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class WalkAction : BaseAction
	{
		private readonly string _walkTextColor;
		private readonly Color _walkColor;
		private readonly Image _walkImage;
		private readonly Parameter<Parameters> _parameter;
		
		public WalkAction(WalkStatus walkStatus)
		{
			_parameter = walkStatus.Parameter;
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

		public override void OnUpdate()
		{
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			Vector2 movement = new Vector2(x, y);
			_parameter.Set(Parameters.Move, movement.magnitude);
			
			// _parameter.Set(Parameters.IsDead, Input.GetKeyDown(KeyCode.Space));
		}

		public override void OnExit()
		{
			_walkImage.gameObject.SetActive(false);
		}
	}
}
