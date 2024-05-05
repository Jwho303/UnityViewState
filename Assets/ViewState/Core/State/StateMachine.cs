using UnityEngine;

namespace State
{
    public class StateMachine
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

            if (_exitingState != null)
            {
                InterruptOngoingTransitions();
            }

            if (CurrentState == null)
            {
                EnterNewState(newState);
            }
            else
            {
                ExitCurrentState();
                EnterNewState(newState);
            }
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

        private void InterruptOngoingTransitions()
        {
            _exitingState.Interupt();
            _exitingState = null;
        }

        private void ExitCurrentState()
        {
            _exitingState = CurrentState;
            _exitingState.ExitState();
            CurrentState = null;
        }

        private void EnterNewState(IState newState)
        {
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}
