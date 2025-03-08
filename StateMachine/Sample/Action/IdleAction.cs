using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class IdleAction : BaseAction
	{
		private readonly Parameter<Parameters> _parameter;
		private readonly string _idleTextColor;
		private readonly Image _idleImage;
		
		public IdleAction(IdleStatus idleStatus)
		{
			_parameter = idleStatus.Parameter;
			_idleTextColor = idleStatus.TextColor;
			_idleImage = idleStatus.Image;
			
			Color idleColor = idleStatus.Color;
			_idleImage.color = idleColor;
		}
		
		public override void OnEnter()
		{
			Debug.Log($"<color={_idleTextColor}>Idle State</color>");
			
			_idleImage.gameObject.SetActive(true);
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
			_idleImage.gameObject.SetActive(false);
		}
	}
}
