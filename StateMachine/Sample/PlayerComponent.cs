// 〇〇Component.cs(PlayerComponent, EnemyComponent)
// GameObjectにアタッチされているコンポーネントをまとめたクラス / Class that summarizes components attached to GameObject

using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent
{
	public Image Idle { get; }
	public Image Walk { get; }
	public Image Dead { get; }
	
	public PlayerComponent(Image idle, Image walk, Image dead)
	{
		Idle = idle;
		Walk = walk;
		Dead = dead;
	}
}
