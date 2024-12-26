using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	public class DeadStatus
	{
		public string TextColor { get; }
		public Color Color { get; }
		public Image Image { get; }
		
		public DeadStatus(PlayerComponent playerComponent, PlayerStatus playerStatus)
		{
			Image = playerComponent.Dead;
			
			TextColor = playerStatus.DeadTextColor;
			Color = playerStatus.DeadColor;
		}
	}
}
