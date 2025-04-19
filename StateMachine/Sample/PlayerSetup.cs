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
	
	public enum Parameters
	{
		Move,		// float
		IsDead		// bool
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
			
			// パラメータの生成
			var parameter = new Parameter<Parameters>();
			
			// コンポーネントの生成
			var playerComponent = new PlayerComponent(parameter, _idleImage, _walkImage, _deadImage);
			
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
			
			// パラメータの設定
			parameter.Add<float>(Parameters.Move, 0f);
			parameter.Add<bool>(Parameters.IsDead, false);
			
			// 状態遷移の追加
			// Walk -> Idle: [W,A,S,D]入力がない
			Transition<MoveState> walkToIdle = _moveStateMachine.AddTransition(MoveState.Walk, MoveState.Idle);
			// Idle -> Walk: [W,A,S,D]入力がある
			Transition<MoveState> idleToWalk = _moveStateMachine.AddTransition(MoveState.Idle, MoveState.Walk);
			// Move -> Dead: [Space]が押された
			Transition<PlayerState> moveToDead = _stateMachine.AddTransition(PlayerState.Move, PlayerState.Dead);
			// Any -> Dead: [Space]が押された
			// Any: どの状態からでも遷移可能
			Transition<PlayerState> anyToDead = _stateMachine.AddAnyTransition(PlayerState.Dead);
			
			// 状態遷移の条件を設定
			var isIdle = new Condition(() => parameter.Get<float>(Parameters.Move) == 0f);
			walkToIdle.AddCondition(isIdle, true);
			idleToWalk.AddCondition(isIdle, false);
			
			// 派生クラス式
			var isDead0 = new ConditionIsDead();
			moveToDead.AddCondition(isDead0, true);
			anyToDead.AddCondition(isDead0, true);
			
			// ラムダ式
			// var isDead1 = new Condition(() => Input.GetKeyDown(KeyCode.Space));
			// moveToDead.AddCondition(isDead1, true);
			
			// パラメータ式
			// var isDead2 = new Condition(() => parameter.Get<bool>(Parameters.IsDead));
			// moveToDead.AddCondition(isDead2, true);
			
			// 初期状態の設定
			_moveStateMachine.SetInitState(MoveState.Idle);
			_stateMachine.SetInitState(PlayerState.Move);
			
			// 初期化
			_stateMachine.Initialize();
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

