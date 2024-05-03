namespace State
{
    public interface IStateMachine
    {
        void ChangeState(IState incomingState);
        void StateExitComplete();
    }
}