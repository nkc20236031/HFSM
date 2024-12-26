using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class IdleStatus
	{
		public string TextColor { get; }
		public Color Color { get; }
		public Image Image { get; }
		
		public IdleStatus(PlayerComponent playerComponent, PlayerStatus playerStatus)
		{
			Image = playerComponent.Idle;
			
			TextColor = playerStatus.IdleTextColor;
			Color = playerStatus.IdleColor;
		}
	}
}
