using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class IdleStatus
	{
		public Parameter<Parameters> Parameter { get; }
		public string TextColor { get; }
		public Color Color { get; }
		public Image Image { get; }
		
		public IdleStatus(PlayerComponent playerComponent, PlayerStatus playerStatus)
		{
			Parameter = playerComponent.Parameter;
			Image = playerComponent.Idle;
			
			TextColor = playerStatus.IdleTextColor;
			Color = playerStatus.IdleColor;
		}
	}
}
