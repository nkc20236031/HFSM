using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RizeLibrary.StateMachine
{
    public class StateMachine<TStateID> : BaseAction
    {
        private readonly Dictionary<TStateID, BaseAction> _states = new();
        private readonly List<Transition<TStateID>> _transitions = new();
        private readonly List<Transition<TStateID>> _anyTransitions = new();
        private List<Transition<TStateID>> _selectTransitions = new();
        private BaseAction _currentState;

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
        /// 初期状態の設定
        /// </summary>
        /// <param name="stateID">状態ID</param>
        public void SetInitState(TStateID stateID)
        {
            // 指定した状態が存在する場合
            if (_states.ContainsKey(stateID))
            {
                // 現在の状態を設定
                _currentState = _states[stateID];
                UpdateAnyTransitions();
            }
            else
            {
                // エラーログを出力
                Debug.LogError($"状態{stateID}が存在しません。");
            }
        }
        
        /// <summary>
        /// 対象の遷移を取得
        /// </summary>
        private void UpdateAnyTransitions()
        {
            _selectTransitions = _transitions.Where(transition => _currentState == _states[transition.FromStateID]).ToList();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            // 初期状態が設定されていない場合
            if (_currentState == null)
            {
                // エラーログを出力
                Debug.LogError("初期状態が設定されていません。");
            }
            else
            {
                // 現在の状態に遷移
                _currentState.OnEnter();
            }
        }

        /// <summary>
        /// 状態の変更
        /// </summary>
        /// <param name="stateID">状態ID</param>
        public void ChangeState(TStateID stateID)
        {
            // 指定した状態が存在する場合
            if (_states.ContainsKey(stateID))
            {
                // 現在の状態と異なる場合
                if (_currentState == _states[stateID]) { return; }

                // 現在の状態を変更
                _currentState?.OnExit();
                SetInitState(stateID);
                _currentState?.OnEnter();
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
            _currentState?.OnEnter();
        }

        public override void OnExit()
        {
            _currentState?.OnExit();
        }

        public override void OnUpdate()
        {
            // トリガーされた遷移がある場合
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

            _currentState?.OnUpdate();
        }

        public override void OnFixedUpdate()
        {
            _currentState?.OnFixedUpdate();
        }

        public override void OnDrawDebug()
        {
            _currentState?.OnDrawDebug();
        }
        
        public override void OnDestroy()
        {
            _currentState?.OnDestroy();
        }
    }
}
