// 〇〇Setup.cs(PlayerSetup, EnemySetup, UISetup)
// StateMachineの設定を行うクラス / Class for configuring StateMachine settings

using UnityEngine;
using UnityEngine.UI;

namespace RizeLibrary.StateMachine
{
	// ステートの定義
	// 例: 対象-> 移動(親ステート)[停滞(サブステート), 歩行], 攻撃, 死亡
	// Player -> Move[Idle, Walk, Jump, Fall], Attack, Dead
	// Enemy  -> Wander, Chase, Attack, Dead
	// UI     -> Play, Pause, GameOver, GameClear
	
	// PlayerState -> Move[Idle, Walk], Dead
	// 親ステートマシンの定義
	public enum PlayerState
	{
		Move,
		Dead
	}

	// 子ステートマシンの定義
	public enum MoveState
	{
		Idle,
		Walk
	}

	public class PlayerSetup : MonoBehaviour
	{
		[Header("Player Config")]
		[SerializeField] private PlayerStatus _playerStatus;
		[SerializeField] private Image _idleImage;
		[SerializeField] private Image _walkImage;
		[SerializeField] private Image _deadImage;
		
		//      StateMachine<型(enum, string, int等)>
		private StateMachine<PlayerState> _stateMachine;
		private StateMachine<MoveState> _moveStateMachine;

		private void Start()
		{
			// ステートマシンの生成
			_stateMachine = new StateMachine<PlayerState>();		// 親ステートマシン
			_moveStateMachine = new StateMachine<MoveState>();		// 子ステートマシン
			
			// コンポーネントの生成
			var playerComponent = new PlayerComponent(_idleImage, _walkImage, _deadImage);
			
			// ステータスの生成
			var idleStatus = new IdleStatus(playerComponent, _playerStatus);
			var walkStatus = new WalkStatus(playerComponent, _playerStatus);
			var deadStatus = new DeadStatus(playerComponent, _playerStatus);
			
			// アクションの生成
			var idleAction = new IdleAction(idleStatus);
			var walkAction = new WalkAction(walkStatus);
			var deadAction = new DeadAction(deadStatus);
			
			// 状態の追加
			_stateMachine.AddState(PlayerState.Move, _moveStateMachine);	// Move子ステートマシンを追加
			
			_stateMachine.AddState(PlayerState.Dead, deadAction);			// Deadステートを追加
			
			_moveStateMachine.AddState(MoveState.Idle, idleAction);			// Idleステートを追加
			_moveStateMachine.AddState(MoveState.Walk, walkAction);			// Walkステートを追加
			
			// 状態遷移の条件を設定
			/* Walk -> Idle: 入力がない
			   Idle -> Walk: 入力がある */
			var isIdle = new Decision(() => Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0);
			_moveStateMachine.AddTransition(MoveState.Walk, MoveState.Idle, isIdle, true);
			_moveStateMachine.AddTransition(MoveState.Idle, MoveState.Walk, isIdle, false);
			
			/* Move -> Dead: スペースキーが押された */
			var isDead = new Decision(() => Input.GetKeyDown(KeyCode.Space));
			_stateMachine.AddTransition(PlayerState.Move, PlayerState.Dead, isDead, true);
			
			// 初期状態の設定
			_moveStateMachine.SetInitState(MoveState.Idle);
			_stateMachine.SetInitState(PlayerState.Move);
			
			// 初期化
			_stateMachine.Init();
		}
		
		private void Update()
		{
			// 親ステートマシンのみ更新
			_stateMachine.OnUpdate();
		}
		
		private void FixedUpdate()
		{
			// 親ステートマシンのみ更新
			_stateMachine.OnFixedUpdate();
		}
	}
}

