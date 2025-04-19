using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RizeLibrary.StateMachine
{
    public class StateMachine<TStateID> : BaseAction
    {
        private readonly Dictionary<TStateID, BaseAction> _states = new();
        private readonly Dictionary<TStateID, Action> _subStates = new();
        private readonly List<Transition<TStateID>> _transitions = new();
        private readonly List<Transition<TStateID>> _anyTransitions = new();
        private List<Transition<TStateID>> _selectTransitions = new();
        
        public BaseAction CurrentState { get; private set; }
        public TStateID DefaultStateID { get; private set; }
        public TStateID CurrentStateID { get; private set; }

        /// <summary>
        /// 状態の追加
        /// </summary>
        /// <param name="stateID">状態ID</param>
        /// <param name="state">状態</param>
        public void AddState(TStateID stateID, BaseAction state)
        {
            // すでに同じ状態が存在する場合
            if (_states.ContainsKey(stateID))
            {
                // エラーログを出力
                Debug.LogError($"状態{stateID}は既に存在します。");
            }
            else
            {
                // 状態を追加
                _states.Add(stateID, state);
            }
        }

        /// <summary>
        /// サブステートの追加
        /// </summary>
        /// <param name="stateID">状態ID</param>
        /// <param name="stateMachine">サブステートマシン</param>
        public void AddState<TSubStateID>(TStateID stateID, StateMachine<TSubStateID> stateMachine)
        {
            // すでに同じ状態が存在する場合
            if (_states.ContainsKey(stateID))
            {
                // エラーログを出力
                Debug.LogError($"状態{stateID}は既に存在します。");
            }
            else
            {
                // 状態を追加
                _states.Add(stateID, stateMachine);
                // サブステートを追加
                _subStates.Add(stateID, stateMachine.Initialize);
            }
        }

        /// <summary>
        /// 初期状態の設定
        /// </summary>
        /// <param name="stateID">状態ID</param>
        public void SetInitState(TStateID stateID)
        {
            if (_states.ContainsKey(stateID))
            {
                // 初期状態を設定
                DefaultStateID = stateID;
            }
            else
            {
                // エラーログを出力
                Debug.LogError($"初期状態{stateID}が存在しません。");
            }
            
            SetChangeState(stateID);
        }
        
        /// <summary>
        /// 対象の遷移を取得
        /// </summary>
        private void UpdateAnyTransitions()
        {
            _selectTransitions = _transitions.Where(transition => CurrentState == _states[transition.FromStateID]).ToList();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            // 初期状態が設定されている場合
            if (DefaultStateID != null)
            {
                // 初期状態を設定
                CurrentState?.OnExit();
                CurrentState = _states[DefaultStateID];
                CurrentState?.OnEnter();
            }
            else
            {
                // エラーログを出力
                Debug.LogError("初期状態が設定されていません。");
            }
        }

        /// <summary>
        /// 状態の変更
        /// </summary>
        /// <param name="stateID">状態ID</param>
        public void ChangeState(TStateID stateID)
        {
            // 指定した状態が存在する場合
            if (_states.TryGetValue(stateID, out BaseAction state))
            {
                // 現在の状態同じ場合
                if (CurrentState == state) { return; }

                // 現在の状態を変更
                CurrentState?.OnExit();
                SetChangeState(stateID);
                CurrentState?.OnEnter();
            }
            else
            {
                // エラーログを出力
                Debug.LogError($"状態{stateID}が存在しません。");
            }
        }
        
        /// <summary>
        /// 状態の変更
        /// </summary>
        /// <param name="stateID">状態ID</param>
        private void SetChangeState(TStateID stateID)
        {
            // 指定した状態が存在する場合
            if (_states.ContainsKey(stateID))
            {
                // 現在の状態を設定
                CurrentStateID = stateID;
                CurrentState = _states[stateID];
                UpdateAnyTransitions();
                // Conditionの初期化
                foreach (var transition in _selectTransitions)
                {
                    transition.ConditionInitialize();
                }
            }
            else
            {
                // エラーログを出力
                Debug.LogError($"状態{stateID}が存在しません。");
            }
        }

        /// <summary>
        /// 遷移の追加
        /// </summary>
        /// <param name="fromStateID">遷移元の状態ID</param>
        /// <param name="toStateID">遷移先の状態ID</param>
        public Transition<TStateID> AddTransition(TStateID fromStateID, TStateID toStateID)
        {
            if (_states.ContainsKey(fromStateID) && _states.ContainsKey(toStateID))
            {
                var transition = new Transition<TStateID>(fromStateID, toStateID);
                _transitions.Add(transition);
                return transition;
            }

            Debug.LogError($"いずれかの状態が存在しないため、{fromStateID}から{toStateID}への遷移を追加できません。");
            return null;
        }
        
        /// <summary>
        /// 遷移の追加
        /// </summary>
        /// <param name="toStateID">遷移先の状態ID</param>
        public AnyTransition<TStateID> AddAnyTransition(TStateID toStateID)
        {
            if (_states.ContainsKey(toStateID))
            {
                var transition = new AnyTransition<TStateID>(toStateID);
                _anyTransitions.Add(transition);
                return transition;
            }

            return null;
        }

        public override void OnEnter()
        {
            CurrentState?.OnEnter();
        }

        public override void OnExit()
        {
            CurrentState?.OnExit();
        }

        public override void OnUpdate()
        {
            // 現在の状態の更新処理
            CurrentState?.OnUpdate();
            
            // トリガーされた遷移がある場合
            CheckTransitions();
        }

        private void CheckTransitions()
        {
            if (_selectTransitions.Count > 0)
            {
                foreach (var transition in _selectTransitions)
                {
                    if (transition.IsTriggered())
                    {
                        // 状態を変更
                        ChangeState(transition.ToStateID);
                        break;
                    }
                }
            }

            if (_anyTransitions.Count > 0)
            {
                foreach (var transition in _anyTransitions)
                {
                    if (transition.IsTriggered())
                    {
                        // 状態を変更
                        ChangeState(transition.ToStateID);
                        break;
                    }
                }
            }
        }

        public override void OnFixedUpdate()
        {
            CurrentState?.OnFixedUpdate();
        }

        public override void OnDrawDebug()
        {
            CurrentState?.OnDrawDebug();
        }
        
        public override void OnDestroy()
        {
            CurrentState?.OnDestroy();
        }
    }
}
