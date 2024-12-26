using System.Collections.Generic;
using System.Linq;

namespace RizeLibrary.StateMachine
{
    public class StateMachine<TStateID> : BaseAction
    {
        private readonly Dictionary<TStateID, BaseAction> _states = new();
        private readonly List<Transition<TStateID>> _transitions = new();
        private List<Transition<TStateID>> _anyTransitions = new();
        private BaseAction _currentState;

        /// <summary>
        /// 状態の追加
        /// </summary>
        /// <param name="stateID">状態のID</param>
        /// <param name="state">状態</param>
        public void AddState(TStateID stateID, BaseAction state)
        {
            _states.Add(stateID, state);
        }

        /// <summary>
        /// 初期状態の設定
        /// </summary>
        /// <param name="stateID">状態のID</param>
        public void SetInitState(TStateID stateID)
        {
            _currentState = _states[stateID];
            UpdateAnyTransitions();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            _currentState?.OnEnter();
        }

        /// <summary>
        /// 状態の変更
        /// </summary>
        /// <param name="stateID">状態のID</param>
        public void ChangeState(TStateID stateID)
        {
            if (_currentState == _states[stateID]) { return; }

            _currentState?.OnExit();
            SetInitState(stateID);
            _currentState?.OnEnter();
        }

        /// <summary>
        /// 遷移の追加
        /// </summary>
        /// <param name="fromStateID">遷移元の状態のID</param>
        /// <param name="toStateID">遷移先の状態のID</param>
        /// <param name="decision">条件</param>
        /// <param name="conditionMet">条件が成立しているか</param>
        public void AddTransition(TStateID fromStateID, TStateID toStateID, Decision decision, bool conditionMet)
        {
            _transitions.Add(new Transition<TStateID>(fromStateID, toStateID, decision, conditionMet));
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
            // 遷移の確認
            Transition<TStateID> transition = _anyTransitions.FirstOrDefault(transition => transition.IsTriggered());
            if (transition != null)
            {
                ChangeState(transition.ToStateID);
            }

            _currentState?.OnUpdate();
        }

        public override void OnFixedUpdate()
        {
            _currentState?.OnFixedUpdate();
        }

        private void UpdateAnyTransitions()
        {
            _anyTransitions = _transitions.Where(transition => _currentState == _states[transition.FromStateID]).ToList();
        }

        public override void OnDraw()
        {
            _currentState?.OnDraw();
        }
    }
}