// 〇〇Component.cs(PlayerComponent, EnemyComponent)
// GameObjectにアタッチされているコンポーネントをまとめたクラス / Class that summarizes components attached to GameObject

using UnityEngine.UI;
using RizeLibrary.StateMachine;

public class PlayerComponent
{
	public Parameter<Parameters> Parameter { get; }
	public Image Idle { get; }
	public Image Walk { get; }
	public Image Dead { get; }
	
	public PlayerComponent(Parameter<Parameters> parameter, Image idle, Image walk, Image dead)
	{
		Parameter = parameter;
		Idle = idle;
		Walk = walk;
		Dead = dead;
	}
}
