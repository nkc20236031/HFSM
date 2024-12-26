using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class WalkStatus
	{
		public string TextColor { get; }
		public Color Color { get; }
		public Image Image { get; }

		public WalkStatus(PlayerComponent playerComponent, PlayerStatus playerStatus)
		{
			Image = playerComponent.Walk;
			
			TextColor = playerStatus.WalkTextColor;
			Color = playerStatus.WalkColor;
		}
	}
}
