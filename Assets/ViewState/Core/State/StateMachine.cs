using UnityEngine;

namespace State
{
    public class StateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }
        private IState _exitingState;

        public StateMachine(IState initialState)
        {
            ChangeState(initialState);
        }

        public void ChangeState(IState newState)
        {
            if (CurrentState == newState)
            {
                return;
            }

            if (CurrentState != null)
            {
                _exitingState = CurrentState;
                _exitingState.ExitState();
            }

            CurrentState = newState;
            CurrentState.EnterState();
        }

        public void Update()
        {
            _exitingState?.UpdateState();
            CurrentState?.UpdateState();
        }

        public void StateExitComplete()
        {
            _exitingState = null;
        }
    }
}