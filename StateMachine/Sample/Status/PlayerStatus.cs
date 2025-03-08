using UnityEngine;

namespace RizeLibrary.StateMachine
{
	[CreateAssetMenu(fileName = "PlayerStatus", menuName = "RizeLibrary/Sample/PlayerStatus")]
	public class PlayerStatus : ScriptableObject
	{
		[Header("Idle Status Settings")]
		public string IdleTextColor = "blue";
		public Color IdleColor = Color.blue;
		
		[Header("Walk Status Settings")]
		public string WalkTextColor = "green";
		public Color WalkColor = Color.green;
		
		[Header("Dead Status Settings")]
		public string DeadTextColor = "red";
		public Color DeadColor = Color.red;
	}
}

